using Microsoft.AspNetCore.Mvc;
using UniqueWord.Api.DataModels;
using UniqueWord.Api.Repositories.Interfaces;

namespace UniqueWord.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchlistRepository _watchlistRepository;

        public WatchListController(IWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        [HttpPost]
        public string PostWatchList(WatchWordData data)
        {
            _watchlistRepository.SaveWatchWord(data.Word);

            return data.Word;
        }
    }
}
