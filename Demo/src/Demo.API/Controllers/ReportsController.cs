using AutoMapper;
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

        // GET api/reports/name-percentage-by-region/{region}
        [HttpGet("name-percentage-by-region/{region}")]
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

        // GET api/reports/family-tree
        [HttpGet("family-tree-of/{level}/for/{firstName}/{lastName}")]
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

        // GET api/reports/get-filtered-report
        [HttpPost("filtered-report")]
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
