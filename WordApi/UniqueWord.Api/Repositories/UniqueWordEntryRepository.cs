using System.Collections.Generic;
using UniqueWord.Api.Factories.Interfaces;
using UniqueWord.Api.Helper.Interfaces;
using UniqueWord.Api.Models;
using UniqueWord.Api.Repositories.Interfaces;

namespace UniqueWord.Api.Repositories
{
    public class UniqueWordEntryRepository : IUniqueWordEntryRepository
    {
        private readonly ISessionHelper _sessionHelper;
        private readonly IModelFactory _modelFactory;

        public UniqueWordEntryRepository(
            ISessionHelper sessionHelper,
            IModelFactory modelFactory)
        {
            _sessionHelper = sessionHelper;
            _modelFactory = modelFactory;
        }

        public UniqueWordEntry Save(TextEntry textEntry, string word)
        {
            return _sessionHelper.WrapInTransaction(s =>
            {
                var uniqueWordEntry = _modelFactory.CreateUniqueWordEntry(textEntry, word);
                s.Save(uniqueWordEntry);
                return uniqueWordEntry;
            });
        }

        public bool BatchSave(TextEntry textEntry, IList<string> words)
        {
            if (words.Count == 0)
                return true;
            return _sessionHelper.WrapInTransaction(s =>
            {
                for (var index = 0; index < words.Count; index++)
                {
                    var word = words[index];
                    var uniqueWordEntry = _modelFactory.CreateUniqueWordEntry(textEntry, word);
                    s.Save(uniqueWordEntry);
                    if (index % 100 == 0)
                    {
                        s.Flush();
                        s.Clear();
                    }
                }
                return true;
            });
        }
    }
}
