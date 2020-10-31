using Demo.Business.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/tests")]
    public class TestController : MainController
    {
        public TestController(INotifier notifier)
            : base (notifier)
        {

        }

        [HttpGet, Route("ping")]
        public async Task<ActionResult> Ping()
        {
            return await Task.Run(() => { return Ok(); });
        }
    }
}
