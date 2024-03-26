using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatsCounter.Models;
using StatsCounter.Services.Interfaces;

namespace StatsCounter.Controllers
{
    [ApiController]
    [Route("users/{owner}/repos")] 
    public class RepositoriesController : ControllerBase
    {
        private readonly IStatsService _statsService;

        public RepositoriesController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [Produces("application/json")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RepositoryStats>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RepositoryStats>>> Get([FromRoute] string owner)
        {
            var result = await _statsService.GetRepositoryStatsByOwnerAsync(owner).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
