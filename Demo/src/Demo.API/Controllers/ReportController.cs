using AutoMapper;
using Demo.API.ViewModels;
using Demo.Contracts.Business;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRegionalReports _regionalReports;
        private readonly IMapper _mapper;
        private readonly IFamilyTreeReports _familyTreeReports;
        private readonly IResearchRepository _researchRepository;

        public ReportController(IRegionalReports regionalReports, IMapper mapper, IFamilyTreeReports familyTreeReports, IResearchRepository researchRepository)
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
            var reportResult = await _regionalReports.GetPercentageByRegionReport(region);

            var responseResult = _mapper.Map<RegionalReportViewModel>(reportResult);

            return Ok(responseResult); // http - 200
        }

        // GET api/reports/get-family-tree/{level}
        [HttpGet, Route("family-tree/{level}/for/{personFullName}")]
        public async Task<ActionResult<dynamic>> GetFamilyTree(string level, string personFullName) // TODO: ajustar tipo de retorno
        {
            var responseResult = await _familyTreeReports.GetFamilyTreeBasedOnLevelByPerson(level, personFullName);

            switch (level.ToLower())
            {
                case "ancestors":
                    return Ok(responseResult ?? _mapper.Map<AncestorsReportViewModel>(responseResult));

                case "children":
                    return Ok(responseResult ?? _mapper.Map<ChildrenReportViewModel>(responseResult));

                case "parents":
                    return Ok(responseResult ?? _mapper.Map<ParentsReportViewModel>(responseResult));

                default:
                    return NoContent();
            }            
        }

        // GET api/reports/get-filtered-report
        [HttpGet, Route("filtered-report")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetFilteredReport([FromBody] FilterObjectViewModel modelFilter)
        {
            // TODO: Implementar validacao do model para garantir a continuidade do processo

            var filter = _mapper.Map<FilterObject>(modelFilter);

            if (!modelFilter.IsGrouped)
            {
                var repositoryResponse = await _researchRepository.GetFilteredResearches(filter);
                var responseResult = _mapper.Map<IEnumerable<ResearchViewModel>>(repositoryResponse);
                return Ok(responseResult); // http - 200
            }
            else
            {
                var repositoryResponse = await _researchRepository.GetFilteredResearchesGrouped(filter);
                var responseResult = _mapper.Map<IEnumerable<FilteredResearchGroupedViewModel>>(repositoryResponse);
                return Ok(responseResult); // http - 200
            }
        }
    }
}
