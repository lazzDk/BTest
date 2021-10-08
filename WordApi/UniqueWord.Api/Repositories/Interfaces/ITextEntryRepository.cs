using UniqueWord.Api.Models;

namespace UniqueWord.Api.Repositories.Interfaces
{
    public interface ITextEntryRepository
    {
        TextEntry SaveTextEntry(int noOfUniqueWords);
    }
}
