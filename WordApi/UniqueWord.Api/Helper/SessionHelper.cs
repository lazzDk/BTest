using System;
using Microsoft.Extensions.Logging;
using NHibernate;
using UniqueWord.Api.Helper.Interfaces;

namespace UniqueWord.Api.Helper
{
    public class SessionHelper : ISessionHelper
    {
        private readonly ISession _session;
        private readonly ILogger<SessionHelper> _logger;

        public SessionHelper(
            ISession session, 
            ILogger<SessionHelper> logger
            )
        {
            _session = session;
            _logger = logger;
        }


        public T WrapInTransaction<T>(Func<ISession, T> action)
        {
            //Ikke implementeret retry 
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var result = action(_session);
                    transaction.Commit();
                    return result;
                }
                catch (Exception exception)
                {
                    if (!transaction.WasRolledBack)
                        transaction.Rollback();
                    _logger.LogError(exception.Message);
                    return default;
                }
            }
        }

        public bool WrapQuery<T>(Func<ISession, T> action, out T result)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    result = action(_session);
                    transaction.Commit();
                    return true;
                }
                catch (Exception exception)
                {
                    if (!transaction.WasRolledBack)
                        transaction.Rollback();
                    _logger.LogError(exception.Message);
                    result = default;
                    return false;
                }
            }
        }

        //STATELESS SESSIONS
    }
}
