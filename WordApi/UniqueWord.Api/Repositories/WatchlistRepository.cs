using System.Collections.Generic;
using System.Linq;
using UniqueWord.Api.Factories.Interfaces;
using UniqueWord.Api.Helper.Interfaces;
using UniqueWord.Api.Models;
using UniqueWord.Api.Repositories.Interfaces;

namespace UniqueWord.Api.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly ISessionHelper _sessionHelper;
        private readonly IModelFactory _modelFactory;

        public WatchlistRepository(
            ISessionHelper sessionHelper, 
            IModelFactory modelFactory)
        {
            _sessionHelper = sessionHelper;
            _modelFactory = modelFactory;
        }

        public IList<string> GetWords()
        {
            return _sessionHelper.WrapQuery(s => s.QueryOver<WatchWord>().List(), out var result) 
                ? result.Select(ww => ww.Word).ToList() 
                : new List<string>();
        }

        public WatchWord SaveWatchWord(string word)
        {
            return _sessionHelper.WrapInTransaction(s =>
            {
                var watchWord = _modelFactory.CreateWatchWord(word);
                 s.Save(watchWord);
                 return watchWord;
            });
        }
    }
}
