using AutoMapper;
using Demo.API.ViewModels;
using Demo.Business.Contracts;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [AllowAnonymous]
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : MainController
    {
        private readonly IRegionalReports _regionalReports;
        private readonly IMapper _mapper;
        private readonly IFamilyTreeReports _familyTreeReports;
        private readonly IResearchRepository _researchRepository;

        public ReportsController(IRegionalReports regionalReports,
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

        // GET api/reports/name-percentage-by-region/{region}
        [HttpGet, Route("name-percentage-by-region/{region}")]
        public async Task<ActionResult<RegionalReportViewModel>> GetPercentageByRegion([FromRoute] string region)
        {
            var result = await _regionalReports.GetNamesPercentageByRegion(region);

            if (result == null || result.Count == 0)
                return NotFound();

            return CustomResponse(_mapper.Map<RegionalReportViewModel>(result));
        }

        // GET api/reports/family-tree
        [HttpGet, Route("family-tree-of/{level}/for/{firstName}/{lastName}")]
        public async Task<ActionResult<dynamic>> GetFamilyTree([FromRoute] string level, string firstName, string lastName)
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
                    NotifyError("Oops, sorry! Something wrong has happened.");
                    return CustomResponse();
            }
        }

        // GET api/reports/get-filtered-report
        [HttpPost, Route("filtered-report/{isGrouped:bool}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetFilteredReport([FromBody] ResearchViewModel modelFilter, bool isGrouped)
        {
            var filter = _mapper.Map<Research>(modelFilter);

            if(string.IsNullOrEmpty(modelFilter.Region) && string.IsNullOrEmpty(modelFilter.Person.FirstName) && 
               string.IsNullOrEmpty(modelFilter.Person.Gender) && string.IsNullOrEmpty(modelFilter.Person.SkinColor) && 
               string.IsNullOrEmpty(modelFilter.Person.Schooling))
            {
                NotifyError("Select one filter at least and try again");
                return CustomResponse();
            }

            if (!isGrouped)
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
