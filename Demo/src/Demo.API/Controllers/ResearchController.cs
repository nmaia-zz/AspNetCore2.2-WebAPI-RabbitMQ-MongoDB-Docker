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
        public async Task<ActionResult<ResearchViewModel>> GetAllResearches()
        {
            var response = await _researchRepository.GetAll();
            return Ok(response); // http - 200
        }

        // POST api/researches/inset-one
        [HttpPost, Route("insert-one")]
        public ActionResult<ResearchViewModel> Post([FromBody] ResearchViewModel model)
        {
            var research = _mapper.Map<Research>(model);

            // TODO: Criar objeto para passar como parametro para o metodo abaixo
            _queueManagementResearch.Publish(research, "demo.queue", "demo.exchange", "demo.queue*");

            return Accepted(model); // http - 202
        }

        // GET api/researches/reports/get-percentage-by-region
        [HttpGet, Route("reports/get-percentage-by-region/{region}")]
        public async Task<ActionResult<RegionalReportViewModel>> GetPercentageByRegion([FromRoute] string region)
        {
            var reportResult = await _regionalReports.GetPercentageByRegionReport(region);

            var responseResult = _mapper.Map<RegionalReportViewModel>(reportResult);

            return Ok(responseResult); // http - 200
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
