using Demo.API.ResponseObjects;
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

        /// <summary>
        /// It represents an endpoint to be consumed by the integration test in order to check if the API is answering the requests.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/tests/ping
        ///
        /// </remarks>
        /// <returns>HttpStatusCode</returns>
        /// <response code="200">Status Code 200 - OK.</response>
        /// <response code="400">If something is wrong in the request.</response>
        [HttpGet("ping")]
        [ProducesResponseType(typeof(OkResponse), 200)]
        [ProducesResponseType(typeof(BadResponse), 400)]
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
