using System.Collections.Generic;
using UniqueWord.Api.Models;

namespace UniqueWord.Api.Repositories.Interfaces
{
    public interface IWatchlistRepository
    {
        IList<string> GetWords();
        WatchWord SaveWatchWord(string word);
    }
}
