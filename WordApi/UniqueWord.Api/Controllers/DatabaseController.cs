using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UniqueWord.Api.Extensions;

namespace UniqueWord.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DatabaseController(IConfiguration configuration )
        {
            _configuration = configuration;
        }

        [HttpPost]
        public string PostCreateDatabase()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            NHibernateExtensions.CreateDatabase(connectionString);
            return "Database created";
        }
    }
}
