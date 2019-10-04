using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheServer.Models;
using TheServer.Services;

namespace TheServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RunnersController : ControllerBase
    {
        private readonly IRunnerService runnerService;

        public RunnersController(IRunnerService runnerService)
            => this.runnerService = runnerService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MongoRunner>>> GetRunners()
        {
            var runners = await runnerService.GetAll();
            return Ok(runners);
        }
    }
}
