using AutoMapper;
using Demo.API.DTO;
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
        private readonly IRegionalReports _regionalReports;
        private readonly IFamilyTreeReports _familyTreeReports;

        public ResearchController(IRepositoryResearch researchRepository, IQueueManagementResearch queueMessageResearch, IMapper mapper, IRegionalReports regionalReports, IFamilyTreeReports familyTreeReports)
        {
            _researchRepository = researchRepository;
            _queueManagementResearch = queueMessageResearch;
            _mapper = mapper;
            _regionalReports = regionalReports;
            _familyTreeReports = familyTreeReports;
        }

        // GET api/researches/list-all
        [HttpGet, Route("list-all")]
        public async Task<ActionResult<IEnumerable<ResearchDto>>> GetAllResearches()
        {
            var response = await _researchRepository.GetAll();
            return Ok(response); // http - 200
        }

        // POST api/researches/inset-one
        [HttpPost, Route("insert-one")]
        public ActionResult<ResearchDto> Post([FromBody] ResearchDto research)
        {
            var item = _mapper.Map<Research>(research);

            // TODO: Criar objeto para passar como parametro para o metodo abaixo
            _queueManagementResearch.Publish(item, "demo.queue", "demo.exchange", "demo.queue*");

            return Accepted(research); // http - 202
        }

        // GET api/researches/reports/get-percentage-by-region
        [HttpGet, Route("reports/get-percentage-by-region")]
        public async Task<ActionResult<double>> GetPercentageByRegion()
        {
            var allResearches = await _researchRepository.GetAll();

            // TODO: Implementar este metodo
            var reportResult = await _regionalReports.PercentageReport(allResearches);

            return Ok(reportResult); // http - 200
        }

        // GET api/researches/reports/get-family-tree
        [HttpGet, Route("reports/get-family-tree")]
        public async Task<ActionResult<double>> GetFamilyTree()
        {
            // TODO: Implementar

            return Ok(); // http - 200
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
