using Microsoft.AspNetCore.Mvc;
using UniqueWord.Api.DataModels;
using UniqueWord.Api.ReturnTypes;
using UniqueWord.Api.Services.Interfaces;

namespace UniqueWord.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniqueWordController : ControllerBase
    {
        private readonly IUniqueWordService _uniqueWordService;

        public UniqueWordController(
            IUniqueWordService uniqueWordService
            )
        {
            _uniqueWordService = uniqueWordService;

        }

        [HttpGet]
        public string GetStatus()
        {
            return "Up and running";
        }

        [HttpPost]
        public UniqueWordResult Post(TextEntryData data)
        {
            if (data == null)
                return UniqueWordResult.ErrorUniqueWordResult("No data");

            var distinctWords = _uniqueWordService.GetDistinctWords(data.Text);
            var watchedWords = _uniqueWordService.GetWatchedWords(distinctWords);
            if (!_uniqueWordService.SaveResults(distinctWords))
                return UniqueWordResult.ErrorUniqueWordResult("Could not save data");

            return new UniqueWordResult
            {
                DistinctUniqueWords = distinctWords.Count,
                WatchListWords = watchedWords
            };
        }
    }
}
