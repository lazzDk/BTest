using System.Collections.Generic;
using UniqueWord.Api.Models;

namespace UniqueWord.Api.Repositories.Interfaces
{
    public interface IUniqueWordEntryRepository
    {
        UniqueWordEntry Save(TextEntry textEntry, string word);
        bool BatchSave(TextEntry textEntry, IList<string> words);

    }
}
