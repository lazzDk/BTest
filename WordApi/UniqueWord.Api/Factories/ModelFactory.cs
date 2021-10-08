using UniqueWord.Api.Factories.Interfaces;
using UniqueWord.Api.Models;

namespace UniqueWord.Api.Factories
{
    public class ModelFactory : IModelFactory
    {
        public WatchWord CreateWatchWord(string word)
        {
            return new WatchWord
            {
                Word = word
            };
        }

        public TextEntry CreateTextEntry(int noOfUniqueWords)
        {
            return new TextEntry
            {
                NoOfWords = noOfUniqueWords
            };
        }

        public UniqueWordEntry CreateUniqueWordEntry(TextEntry textEntry, string word)
        {
            return new UniqueWordEntry(textEntry)
            {
                Word = word
            };
        }
    }
}
