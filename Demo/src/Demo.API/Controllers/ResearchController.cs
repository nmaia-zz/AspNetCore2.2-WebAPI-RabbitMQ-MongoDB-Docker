using AutoMapper;
using Demo.API.ViewModels;
using Demo.Contracts.Business;
using Demo.Contracts.RabbitMQ;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    // TODO: Incluir o uso do polly para garantir a disponibilidade da API
    // TODO: Configurar o JWT para o uso de claims no consumo dos endpoints
    // TODO: Configurar o swagger
    // TODO: Criar convencions e configurar analyzers para as controllers da API
    [Route("api/researches")]
    [ApiController]
    public class ResearchController : ControllerBase
    {
        private readonly IQueueManagementResearch _queueManagementResearch;
        private readonly IRepositoryResearch _researchRepository;
        private readonly IMapper _mapper;
        
        // TODO: Criar uma nova controller apenas para os reports
        private readonly IRegionalReports _regionalReports;
        private readonly IAncestorsReports _ancestorsReports;
        private readonly IChildrenReports _childrenReports;
        private readonly IParentsReports _parentsReports;

        public ResearchController(IRepositoryResearch researchRepository
            , IQueueManagementResearch queueMessageResearch
            , IMapper mapper
            , IRegionalReports regionalReports
            , IAncestorsReports familyTreeReports, IChildrenReports childrenReports, IParentsReports parentsReports)
        {
            _researchRepository = researchRepository;
            _queueManagementResearch = queueMessageResearch;
            _mapper = mapper;
            _regionalReports = regionalReports;
            _ancestorsReports = familyTreeReports;
            _childrenReports = childrenReports;
            _parentsReports = parentsReports;
        }

        // GET api/researches/list-all
        [HttpGet, Route("list-all")]
        public async Task<ActionResult<IEnumerable<ResearchViewModel>>> GetAllResearches()
        {
            var allResearches = await _researchRepository.GetAll();

            var model = _mapper.Map<IEnumerable<ResearchViewModel>>(allResearches);

            return Ok(model); // http - 200
        }

        // POST api/researches/inset-one
        [HttpPost, Route("insert-one")]
        public ActionResult<ResearchViewModel> InsertOneResearch([FromBody] ResearchViewModel model)
        {
            var research = _mapper.Map<Research>(model);

            // TODO: Criar objeto para passar como parametro para o metodo abaixo
            _queueManagementResearch.Publish(research, "demo.queue", "demo.exchange", "demo.queue*");

            return Accepted(model); // http - 202
        }

        // GET api/researches/reports/get-percentage-by-region/{region}
        [HttpGet, Route("reports/get-percentage-by-region/{region}")]
        public async Task<ActionResult<RegionalReportViewModel>> GetPercentageByRegion([FromRoute] string region)
        {
            var reportResult = await _regionalReports.GetPercentageByRegionReport(region);

            var responseResult = _mapper.Map<RegionalReportViewModel>(reportResult);

            return Ok(responseResult); // http - 200
        }

        // GET api/researches/reports/get-family-tree/{level}
        [HttpGet, Route("reports/get-family-tree-at-level/{level}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetFamilyTree(string level) // TODO: ajustar tipo de retorno
        {
            IEnumerable<AncestorsReportViewModel> responseAncestorsResult;
            IEnumerable<ChildrenReportViewModel> responseChildrenResult;
            IEnumerable<ParentsReportViewModel> responseParentsResult;

            // TODO: usar polimorfismo aqui ou criar uma rota para cada report
            switch (level)
            {
                case "ancestors":
                    var reportAncestorsResult = await _ancestorsReports.GetAncestorsReport();
                    responseAncestorsResult = _mapper.Map<IEnumerable<AncestorsReportViewModel>>(reportAncestorsResult);
                    return Ok(responseAncestorsResult);

                case "children":
                    var reportChildrenResult = await _childrenReports.GetChildrenReport();
                    responseChildrenResult = _mapper.Map<IEnumerable<ChildrenReportViewModel>>(reportChildrenResult);
                    return Ok(responseChildrenResult);

                case "parents":
                    var reportParentsResult = await _parentsReports.GetParentsReport();
                    responseParentsResult = _mapper.Map<IEnumerable<ParentsReportViewModel>>(reportParentsResult);
                    return Ok(responseParentsResult);

                default:
                    return BadRequest(); // http - 400
            }
        }

        // GET api/researches/reports/get-filtered-report
        [HttpGet, Route("reports/get-filtered-report/{filter}")]
        public async Task<ActionResult<double>> GetFilteredReport([FromQuery] string filter)
        {
            // TODO: Implementar

            return Ok(); // http - 200
        }
    }
}
