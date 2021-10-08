using System.Collections.Generic;

namespace UniqueWord.Api.Services.Interfaces
{
    public interface IUniqueWordService
    {
        IList<string> GetDistinctWords(string text);
        IList<string> GetWatchedWords(IEnumerable<string> distinctWords);
        bool SaveResults(IList<string> distinctWords);
    }
}
