using UniqueWord.Api.Factories.Interfaces;
using UniqueWord.Api.Helper.Interfaces;
using UniqueWord.Api.Models;
using UniqueWord.Api.Repositories.Interfaces;

namespace UniqueWord.Api.Repositories
{
    public class TextEntryRepository: ITextEntryRepository
    {
        private readonly ISessionHelper _sessionHelper;
        private readonly IModelFactory _modelFactory;

        public TextEntryRepository(
            ISessionHelper sessionHelper,
            IModelFactory modelFactory)
        {
            _sessionHelper = sessionHelper;
            _modelFactory = modelFactory;
        }

        public TextEntry SaveTextEntry(int noOfUniqueWords)
        {
            return _sessionHelper.WrapInTransaction(s =>
            {
                var entry = _modelFactory.CreateTextEntry(noOfUniqueWords);
                s.Save(entry);
                return entry;
            });
        }
    }
}