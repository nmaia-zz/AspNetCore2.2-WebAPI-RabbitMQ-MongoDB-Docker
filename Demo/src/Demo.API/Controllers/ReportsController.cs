using AutoMapper;
using Demo.API.ResponseObjects;
using Demo.API.ViewModels;
using Demo.Business.Contracts;
using Demo.Infra.Contracts.Repository;
using Demo.Infra.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [Route("api/reports")]
    public class ReportsController : MainController
    {
        private readonly IRegionalReports _regionalReports;
        private readonly IMapper _mapper;
        private readonly IFamilyTreeReports _familyTreeReports;
        private readonly IResearchRepository _researchRepository;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IRegionalReports regionalReports,
                                IMapper mapper,
                                IFamilyTreeReports familyTreeReports,
                                IResearchRepository researchRepository,
                                INotifier notifier, ILogger<ReportsController> logger) : base(notifier)
        {
            _regionalReports = regionalReports;
            _mapper = mapper;
            _familyTreeReports = familyTreeReports;
            _researchRepository = researchRepository;
            _logger = logger;
        }

        /// <summary>
        /// Returns the percentage of same names by region.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/reports/name-percentage-by-region/{region}
        /// 
        /// Enum values available for the {region} parameter (must be string):
        /// 
        ///     NORTHEST_REGION = 1,
        ///     NORTH_REGION = 2,
        ///     MIDWEST_REGION = 3,
        ///     SOUTHEAST_REGION = 4,
        ///     SOUTH_REGION = 5
        /// 
        /// </remarks>
        /// <param name="region">Represents the available regions in the API.</param>
        /// <returns>A dictionary containing all the names and its percentage by region.</returns>
        /// <response code="200">A dictionary with the names and percentages for each name by region.</response>
        /// <response code="400">If something is wrong in the process.</response>
        /// <response code="404">If there is no data found.</response>
        [HttpGet("name-percentage-by-region/{region}")]
        [ProducesResponseType(typeof(OkResponse), 200)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<ActionResult<Dictionary<string, decimal>>> GetPercentageByRegion([FromRoute] string region)
        {
            try
            {
                var result = await _regionalReports.GetNamesPercentageByRegion(region);

                if (result == null || result.Count == 0)
                    return NotFound();

                return CustomResponse(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occurred while processing the method: Task<ActionResult<Dictionary<string, decimal>>> GetPercentageByRegion([FromRoute] string region:{region})");
                _logger.LogError(ex.Message, ex.StackTrace);

                NotifyError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Returns the family tree for the person based on the passed level (ancestors, children or parents).
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/reports/family-tree-of/{level}/for/{firstName}/{lastName}
        /// 
        /// Available values for the {level} parameter (string value):
        /// 
        ///     "ancestors",
        ///     "children",
        ///     "parents"
        /// 
        /// </remarks>
        /// <param name="level">The level in the family tree (ancestors, children or parents).</param>
        /// <param name="firstName">The person first name.</param>
        /// <param name="lastName">The person last name.</param>
        /// <returns>The family tree based on the passed level.</returns>
        /// <response code="200">Returns the person family treen based on the passed level.</response>
        /// <response code="400">If something is wrong in the process.</response>
        /// <response code="404">If there is no data found.</response>
        [HttpGet("family-tree-of/{level}/for/{firstName}/{lastName}")]
        [ProducesResponseType(typeof(OkResponse), 200)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<ActionResult<dynamic>> GetFamilyTree([FromRoute] string level, string firstName, string lastName)
        {
            try
            {
                var personFullName = $"{firstName} {lastName}";
                var responseResult = await _familyTreeReports.GetFamilyTreeBasedOnLevelByPerson(level, personFullName);

                if (responseResult == null)
                    return NotFound();

                switch (level.ToLower())
                {
                    case "ancestors":
                        return CustomResponse(responseResult ?? _mapper.Map<AncestorsReportViewModel>(responseResult));

                    case "children":
                        return CustomResponse(responseResult ?? _mapper.Map<ChildrenReportViewModel>(responseResult));

                    case "parents":
                        return CustomResponse(responseResult ?? _mapper.Map<ParentsReportViewModel>(responseResult));

                    default:
                        Notify("The informed level is not supported, please choose another one and try again.");
                        return CustomResponse();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occurred while processing the method: Task<ActionResult<dynamic>> GetFamilyTree([FromRoute] string level:{level}, string firstName:{firstName}, string lastName:{lastName})");
                _logger.LogError(ex.Message, ex.StackTrace);
                
                NotifyError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Returns a list of researches, filtered or not.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/reports/get-filtered-report
        /// 
        /// Enum values available for the {region} parameter (must be string):
        /// 
        ///     NORTHEST_REGION = 1,
        ///     NORTH_REGION = 2,
        ///     MIDWEST_REGION = 3,
        ///     SOUTHEAST_REGION = 4,
        ///     SOUTH_REGION = 5
        ///     
        /// Enum values available for the {Gender} property (must be string):
        /// 
        ///     MALE = 1,
        ///     FEMALE = 2
        /// 
        /// Enum values available for the {SkinColor} property (must be string):
        /// 
        ///     ALBINO = 1,
        ///     WHITE = 2,
        ///     YELLOW = 3,
        ///     OLIVE = 4,
        ///     BROWN = 5,
        ///     BLACK = 6,
        ///     BURNT = 7
        /// 
        /// Enum values available for the {Schooling} property (must be string):
        /// 
        ///     PHD = 1,
        ///     MASTERS = 2,       
        ///     POSTGRADUATE = 3,
        ///     UNIVERSITY_EDUCATION = 4,
        ///     ELEMENTARY_SCHOOL = 5,
        ///     NONE = 6
        /// 
        /// </remarks>
        /// <param name="modelFilter">Represents an object that will be used to filter the researches in the database.</param>
        /// <returns>Returns a list of researches, filtered or not.</returns>
        /// <response code="200">Returns the person family treen based on the passed level.</response>
        /// <response code="400">If something is wrong in the process.</response>
        /// <response code="404">If there is no data found.</response>
        [HttpPost("filtered-report")]
        [ProducesResponseType(typeof(OkResponse), 200)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetFilteredReport([FromBody] FilterObjectViewModel modelFilter)
        {
            try
            {
                var filter = _mapper.Map<FilterObjectDto>(modelFilter);

                if (string.IsNullOrEmpty(modelFilter.Region) && string.IsNullOrEmpty(modelFilter.FirstName) &&
                   string.IsNullOrEmpty(modelFilter.Gender) && string.IsNullOrEmpty(modelFilter.SkinColor) &&
                   string.IsNullOrEmpty(modelFilter.Schooling))
                {
                    NotifyError("Select one filter at least and try again");
                    return CustomResponse();
                }

                if (!filter.IsGrouped)
                {
                    var researches = await _researchRepository.GetFilteredResearches(filter);

                    if (researches == null || researches.Count() == 0)
                        return NotFound();

                    return CustomResponse(_mapper.Map<IEnumerable<ResearchViewModel>>(researches));
                }
                else
                {
                    var groupedResearches = await _researchRepository.GetFilteredResearchesGrouped(filter);

                    if (groupedResearches == null || groupedResearches.Count() == 0)
                        return NotFound();

                    return CustomResponse(_mapper.Map<IEnumerable<FilteredResearchGroupedViewModel>>(groupedResearches));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error has occurred while processing the method: Task<ActionResult<IEnumerable<dynamic>>> GetFilteredReport([FromBody] FilterObjectViewModel modelFilter: {modelFilter})");
                _logger.LogError(ex.Message, ex.StackTrace);

                NotifyError(ex.Message);
                return CustomResponse();
            }
        }
    }
}
