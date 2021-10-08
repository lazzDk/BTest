using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using UniqueWord.Api.DataModels;
using UniqueWord.Api.Repositories.Interfaces;
using UniqueWord.Api.Services.Interfaces;

namespace UniqueWord.Api.Services
{
    public class UniqueWordService : IUniqueWordService
    {
        private readonly StringComparer _stringComparer = StringComparer.CurrentCultureIgnoreCase;

        private readonly ILogger<UniqueWordService> _logger;
        private readonly IWatchlistRepository _watchlistRepository;
        private readonly IUniqueWordEntryRepository _uniqueWordEntryRepository;
        private readonly ITextEntryRepository _textEntryRepository;

        public UniqueWordService(
            ILogger<UniqueWordService> logger,
             IWatchlistRepository watchlistRepository,
            ITextEntryRepository textEntryRepository,
            IUniqueWordEntryRepository uniqueWordEntryRepository)
        {
            _logger = logger;
            _watchlistRepository = watchlistRepository;
            _uniqueWordEntryRepository = uniqueWordEntryRepository;
            _textEntryRepository = textEntryRepository;
        }

        public IList<string> GetDistinctWords(string text)
        {
            var wordRegex = new Regex(@"[\p{L}']+");
            var words = wordRegex.Matches(text).Select(c => c.Value);
            return words.Distinct(_stringComparer).ToList();
        }


        public IList<string> GetWatchedWords(IEnumerable<string> distinctWords)
        {
            var watchListWords = _watchlistRepository.GetWords();
            return watchListWords.Intersect(distinctWords, _stringComparer).ToList();
        }


      

        public bool SaveResults(IList<string> distinctWords)
        {
            var textEntry = _textEntryRepository.SaveTextEntry(distinctWords.Count);
            if (textEntry == null)
            {
                _logger.LogError("Could not save TextEntry");
                return false;
            }

            if (_uniqueWordEntryRepository.BatchSave(textEntry, distinctWords)) 
                return true;
            _logger.LogError("Could not save unique words");
            return false;

        }
    }
}
