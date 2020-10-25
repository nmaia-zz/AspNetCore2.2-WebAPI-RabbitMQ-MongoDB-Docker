using AutoMapper;
using Demo.API.ViewModels;
using Demo.Contracts.Business;
using Demo.Contracts.RabbitMQ;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    // TODO: Incluir o uso do polly para garantir a resiliencia da API
    // TODO: Configurar o swagger + comentarios para documentacao apropriada da API
    // TODO: Criar convencions e configurar analyzers para as controllers da API
    // TODO: Criar tratamento de excecao
    [EnableCors("AllowAnyOrigin")]
    [Route("api/researches")]
    [ApiController]
    public class ResearchController : ControllerBase
    {
        private readonly IQueueManagementResearch _queueManagementResearch;
        private readonly IResearchRepository _researchRepository;
        private readonly IMapper _mapper;
        private readonly IAncestorsReportsPublisher _ancestorsReports;
        private readonly IChildrenReportsPublisher _childrenReports;
        private readonly IParentsReportsPublisher _parentsReports;

        public ResearchController(IResearchRepository researchRepository
            , IQueueManagementResearch queueMessageResearch
            , IMapper mapper
            , IAncestorsReportsPublisher ancestorsReports
            , IChildrenReportsPublisher childrenReports
            , IParentsReportsPublisher parentsReports)
        {
            _researchRepository = researchRepository;
            _queueManagementResearch = queueMessageResearch;
            _mapper = mapper;
            _ancestorsReports = ancestorsReports;
            _childrenReports = childrenReports;
            _parentsReports = parentsReports;
        }

        // GET api/researches/list-all
        [HttpGet, Route("list-all")]
        public async Task<ActionResult<IEnumerable<ResearchViewModel>>> GetAllResearches()
        {
            var allResearches = await _researchRepository.GetAllAsync();

            var model = _mapper.Map<IEnumerable<ResearchViewModel>>(allResearches);

            return Ok(model); // http - 200
        }

        // POST api/researches/inset-one
        [HttpPost, Route("insert-one")]
        public async Task<ActionResult<ResearchViewModel>> InsertOneResearch([FromBody] ResearchViewModel model)
        {
            // TODO: Incluir forma de validar o model para garantir a fluxo do processo.            
            var research = _mapper.Map<Research>(model);
            await _queueManagementResearch.Publish(research, "researches.queue", "researches.exchange", "researches.queue*");

            await _childrenReports.PublishToBeAddedIntoFamilyTree(research);
            await _parentsReports.PublishToBeAddedIntoFamilyTree(research);
            await _ancestorsReports.PublishToBeAddedIntoFamilyTree(research);

            return Accepted(model); // http - 202
        }
    }
}
