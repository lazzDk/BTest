using UniqueWord.Api.Models;

namespace UniqueWord.Api.Factories.Interfaces
{
    public interface IModelFactory
    {
        WatchWord CreateWatchWord(string word);
        TextEntry CreateTextEntry(int noOfUniqueWords);
        UniqueWordEntry CreateUniqueWordEntry(TextEntry textEntry, string word);
    }
}
