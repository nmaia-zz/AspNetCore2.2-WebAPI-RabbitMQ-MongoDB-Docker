using Demo.Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Demo.API.Controllers
{
    [Route("api/tests")]
    public class TestsController : MainController
    {
        private readonly ILogger<TestsController> _logger;

        public TestsController(INotifier notifier, ILogger<TestsController> logger)
            : base(notifier)
        {
            _logger = logger;
        }

        [HttpGet, Route("ping")]
        public ActionResult<string> Ping()
        {
            try
            {
                return CustomResponse("Request completed with Success.");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occurred while processing the method: ActionResult<string> Ping()");
                _logger.LogError(ex.Message, ex.StackTrace);

                NotifyError(ex.Message);
                return CustomResponse();
            }
        }
    }
}
