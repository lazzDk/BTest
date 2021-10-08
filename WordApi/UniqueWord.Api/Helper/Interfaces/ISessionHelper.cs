using System;
using NHibernate;

namespace UniqueWord.Api.Helper.Interfaces
{
    public interface ISessionHelper
    {
        T WrapInTransaction<T>(Func<ISession, T> action);
        bool WrapQuery<T>(Func<ISession, T> action, out T result);
    }
}
