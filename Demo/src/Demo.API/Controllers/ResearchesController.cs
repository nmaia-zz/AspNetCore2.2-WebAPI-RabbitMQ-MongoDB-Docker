using AutoMapper;
using Demo.API.ResponseObjects;
using Demo.API.ViewModels;
using Demo.Business.Contracts;
using Demo.Business.Contracts.Services;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [Route("api/researches")]
    public class ResearchesController : MainController
    {
        private readonly IResearchRepository _researchRepository;        
        private readonly IResearchServices _researchServices;
        private readonly IAncestorsTreeServices _ancestorsTreeServices;
        private readonly IChildrenTreeServices _childrenTreeServices;
        private readonly IParentsTreeServices _parentsTreeServices;
        private readonly IMapper _mapper;
        private readonly ILogger<ResearchesController> _logger;

        public ResearchesController(IResearchRepository researchRepository,
                                  IMapper mapper,
                                  IResearchServices researchServices,
                                  INotifier notifier, 
                                  IAncestorsTreeServices ancestorsTreeServices, 
                                  IChildrenTreeServices childrenTreeServices, 
                                  IParentsTreeServices parentsTreeServices,
                                  ILogger<ResearchesController> logger) 
            : base(notifier)
        {
            _researchRepository = researchRepository;
            _mapper = mapper;
            _researchServices = researchServices;
            _ancestorsTreeServices = ancestorsTreeServices;
            _childrenTreeServices = childrenTreeServices;
            _parentsTreeServices = parentsTreeServices;
            _logger = logger;
        }

        /// <summary>
        /// Returns all the existing researches.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/researches/list-all
        ///
        /// </remarks>
        /// <returns>A list of existing researches.</returns>
        /// <response code="200">A list of existing researches.</response>
        /// <response code="400">If something is wrong in the process.</response>
        /// <response code="404">If the research is not found.</response>
        [HttpGet("list-all")]
        [ProducesResponseType(typeof(OkResponse), 200)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<ActionResult<IEnumerable<ResearchViewModel>>> GetAllResearches()
        {
            try
            {
                var allResearches = await _researchRepository.GetAllAsync();

                if (allResearches == null || allResearches.Count() == 0)
                    return NotFound();

                var model = _mapper.Map<IEnumerable<ResearchViewModel>>(allResearches);

                return CustomResponse(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occurred while processing the method: Task<ActionResult<IEnumerable<ResearchViewModel>>> GetAllResearches()");
                _logger.LogError(ex.Message, ex.StackTrace);

                NotifyError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Publish the research into the queue to be consumed by a hosted service.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/researches/insert-one
        ///     
        ///     {
        ///     	"id": "",
        ///     	"region": "SOUTHEAST_REGION",
        ///     	"person": {
        ///     		"id": "",
        ///     		"firstName": "Zakk",
        ///     		"lastName": "Wylde",
        ///     		"gender": "MALE",
        ///     		"skinColor": "WHITE",
        ///     		"filiation": [
        ///     		    "Jerome F. Wielandt",
        ///     		    "Someone Wielandt"
        ///     		],
        ///     		"children": [
        ///     		    "Hayley-Rae",
        ///     		    "Jesse John Michael",
        ///     		    "Hendrix Halen Michael Rhoads",
        ///     		    "Sabbath Page"
        ///             ],
        ///     		"schooling": "UNIVERSITY_EDUCATION"
        ///     	}
        ///     }
        ///     
        /// </remarks>
        /// <param name="model">An object that represents the research.</param>
        /// <returns>Returns the registred object into the RabbitMQ queue.</returns>
        /// <returns>Returns the model object that was inserted into the RabbitMQ queue.</returns>
        /// <response code="202">It indicates the research was successfully inserted into RabbitMQ in order to be consumed and registered into the database.</response>
        /// <response code="400">If something is wrong in the process.</response>
        [HttpPost("insert-one")]
        [ProducesResponseType(typeof(AcceptedResult), 202)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        public async Task<ActionResult<ResearchViewModel>> InsertOneResearch([FromBody] ResearchViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                model.Id = ObjectId.GenerateNewId().ToString();
                model.Person.Id = ObjectId.GenerateNewId().ToString();

                var research = _mapper.Map<Research>(model);

                var researchHasBeenPublished = await _researchServices.PublishResearch(research);

                if (researchHasBeenPublished)
                {
                    if (research.Person.Children.Any())
                        await _childrenTreeServices.PublishChildrenFamilyTree(research);

                    await _parentsTreeServices.PublishParentsFamilyTree(research);
                    await _ancestorsTreeServices.PublishAncestorsFamilyTree(research);
                }

                return Accepted(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occurred while processing the method: Task<ActionResult<ResearchViewModel>> InsertOneResearch([FromBody] ResearchViewModel model: {model})");
                _logger.LogError(ex.Message, ex.StackTrace);

                NotifyError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Publish a list of researches (one by one) into the queue to be consumed by a hosted service.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/researches/insert-many
        ///     
        ///     [{
        ///     	"id": "",
        ///     	"region": "SOUTHEAST_REGION",
        ///     	"person": {
        ///     		"id": "",
        ///     		"firstName": "Zakk",
        ///     		"lastName": "Wylde",
        ///     		"gender": "MALE",
        ///     		"skinColor": "WHITE",
        ///     		"filiation": [
        ///     		    "Jerome F. Wielandt",
        ///     		    "Someone Wielandt"
        ///     		],
        ///     		"children": [
        ///     		    "Hayley-Rae",
        ///     		    "Jesse John Michael",
        ///     		    "Hendrix Halen Michael Rhoads",
        ///     		    "Sabbath Page"
        ///             ],
        ///     		"schooling": "UNIVERSITY_EDUCATION"
        ///     	}
        ///     }]
        ///     
        /// </remarks>
        /// <param name="model">An object list that represents a list of researches.</param>
        /// <returns>Returns all the published objects into the RabbitMQ queue.</returns>
        /// <response code="202">It indicates the researches were successfully inserted into the RabbitMQ in order to be consumed and registered into the database.</response>
        /// <response code="400">If something is wrong in the process.</response>
        [HttpPost("insert-many")]
        [ProducesResponseType(typeof(AcceptedResult), 202)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        public async Task<ActionResult<IEnumerable<ResearchViewModel>>> InsertManyResearches([FromBody] IEnumerable<ResearchViewModel> model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                foreach (var item in model)
                {
                    item.Id = ObjectId.GenerateNewId().ToString();
                    item.Person.Id = ObjectId.GenerateNewId().ToString();
                }

                var researches = _mapper.Map<IEnumerable<Research>>(model);
                
                await publishResearchesAndFamilyTrees(researches);

                return Accepted(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occurred while processing the method: Task<ActionResult<IEnumerable<ResearchViewModel>>> InsertManyResearches([FromBody] IEnumerable<ResearchViewModel> model: {model})");
                _logger.LogError(ex.Message, ex.StackTrace);

                NotifyError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// This method is used to publish each research in the list of researches into the RabbitMQ queue.
        /// </summary>
        /// <param name="researches">A list of researches</param>
        private async Task publishResearchesAndFamilyTrees(IEnumerable<Research> researches)
        {
            bool researchHasBeenPublished;

            foreach (var research in researches)
            {
                try
                {
                    researchHasBeenPublished = await _researchServices.PublishResearch(research);
                    
                    await PublishFamilyTrees(research, researchHasBeenPublished);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"We couldn't publish the following research: {research}");
                    _logger.LogError(ex.Message, ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// This method is used to publish the family tree (ancestors, parents and children) after the research is published without errors.
        /// This method is executed only for 'insert-many' endpoint iteration.
        /// </summary>
        /// <param name="research">The research that contains the family tree information.</param>
        /// <param name="canPublish">Indicates if the family tree can be published.</param>
        private async Task PublishFamilyTrees(Research research, bool canPublish)
        {
            if (canPublish)
            {
                if (research.Person.Children.Any())
                    await _childrenTreeServices.PublishChildrenFamilyTree(research);

                await _parentsTreeServices.PublishParentsFamilyTree(research);
                await _ancestorsTreeServices.PublishAncestorsFamilyTree(research);
            }
            else
                return;
        }
    }
}
