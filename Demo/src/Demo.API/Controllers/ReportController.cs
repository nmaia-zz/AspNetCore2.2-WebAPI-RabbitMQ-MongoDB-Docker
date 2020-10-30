using AutoMapper;
using Demo.API.ViewModels;
using Demo.Business.Contracts;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Route("api/reports")]
    [ApiController]
    public class ReportController : MainController
    {
        private readonly IRegionalReports _regionalReports;
        private readonly IMapper _mapper;
        private readonly IFamilyTreeReports _familyTreeReports;
        private readonly IResearchRepository _researchRepository;

        public ReportController(IRegionalReports regionalReports,
                                IMapper mapper,
                                IFamilyTreeReports familyTreeReports,
                                IResearchRepository researchRepository,
                                INotifier notifier) : base(notifier)
        {
            _regionalReports = regionalReports;
            _mapper = mapper;
            _familyTreeReports = familyTreeReports;
            _researchRepository = researchRepository;
        }

        // GET api/reports/get-percentage-by-region/{region}
        [HttpGet, Route("percentage-by-region/{region}")]
        public async Task<ActionResult<RegionalReportViewModel>> GetPercentageByRegion([FromRoute] string region)
        {
            var regionalReport = await _regionalReports.GetPercentageByRegionReport(region);

            if (regionalReport == null || regionalReport.PercentagePerName.Count == 0)
                return NotFound();

            return CustomResponse(_mapper.Map<RegionalReportViewModel>(regionalReport));
        }

        // GET api/reports/get-family-tree/{level}
        [HttpGet, Route("family-tree/{level}/for/{personFullName}")]
        public async Task<ActionResult<dynamic>> GetFamilyTree(string level, string personFullName)
        {
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
                    NotifyError("Oops, sorry! Something wrong has happened.");
                    return CustomResponse();
            }
        }

        // GET api/reports/get-filtered-report
        [HttpGet, Route("filtered-report")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetFilteredReport([FromBody] FilterObjectViewModel modelFilter)
        {
            var filter = _mapper.Map<FilterObject>(modelFilter);

            if(string.IsNullOrEmpty(modelFilter.Region) && string.IsNullOrEmpty(modelFilter.FirstName) && 
               string.IsNullOrEmpty(modelFilter.Gender) && string.IsNullOrEmpty(modelFilter.SkinColor) && 
               string.IsNullOrEmpty(modelFilter.Schooling))
            {
                NotifyError("Select one filter at least and try again");
                return CustomResponse();
            }

            if (!modelFilter.IsGrouped)
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
    }
}
