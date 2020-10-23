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
        private readonly IQueueManagementAncestorReport _queueManagementAncestors;
        private readonly IQueueManagementChildrenReport _queueManagementChildren;
        private readonly IQueueManagementParentsReport _queueManagementParents;
        private readonly IResearchRepository _researchRepository;
        private readonly IMapper _mapper;
        
        // TODO: Criar uma nova controller apenas para os reports
        private readonly IRegionalReports _regionalReports;
        private readonly IAncestorsReports _ancestorsReports;
        private readonly IChildrenReports _childrenReports;
        private readonly IParentsReports _parentsReports;

        public ResearchController(IResearchRepository researchRepository
            , IQueueManagementResearch queueMessageResearch
            , IMapper mapper
            , IRegionalReports regionalReports
            , IAncestorsReports familyTreeReports, IChildrenReports childrenReports, IParentsReports parentsReports, IQueueManagementAncestorReport queueManagementAncestors, IQueueManagementChildrenReport queueManagementChildren, IQueueManagementParentsReport queueManagementParents)
        {
            _researchRepository = researchRepository;
            _queueManagementResearch = queueMessageResearch;
            _mapper = mapper;
            _regionalReports = regionalReports;
            _ancestorsReports = familyTreeReports;
            _childrenReports = childrenReports;
            _parentsReports = parentsReports;
            _queueManagementAncestors = queueManagementAncestors;
            _queueManagementChildren = queueManagementChildren;
            _queueManagementParents = queueManagementParents;
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
            var ancestors = _ancestorsReports.MountAncestorObjectToInsert(research);
            var children = _childrenReports.MountChildrenObjectToInsert(research);
            var parents = _parentsReports.MountParentsObjectToInsert(research);

            // TODO: Criar objeto para passar como parametro para o metodo abaixo
            _queueManagementResearch.Publish(research, "researches.queue", "researches.exchange", "researches.queue*");
            _queueManagementAncestors.Publish(ancestors, "ancestors.queue", "ancestors.exchange", "ancestors.queue*");
            _queueManagementChildren.Publish(children, "children.queue", "children.exchange", "children.queue*");
            _queueManagementParents.Publish(parents, "parents.queue", "parents.exchange", "parents.queue*");

            return Accepted(model); // http - 202
        }

        // GET api/researches/reports/get-percentage-by-region/{region}
        [HttpGet, Route("reports/percentage-by-region/{region}")]
        public async Task<ActionResult<RegionalReportViewModel>> GetPercentageByRegion([FromRoute] string region)
        {
            var reportResult = await _regionalReports.GetPercentageByRegionReport(region);

            var responseResult = _mapper.Map<RegionalReportViewModel>(reportResult);

            return Ok(responseResult); // http - 200
        }

        // GET api/researches/reports/get-family-tree/{level}
        [HttpGet, Route("reports/family-tree/{level}/for/{personFullName}")]
        public async Task<ActionResult<dynamic>> GetFamilyTree(string level, string personFullName) // TODO: ajustar tipo de retorno
        {
            AncestorsReportViewModel responseAncestorsResult;
            ChildrenReportViewModel responseChildrenResult;
            ParentsReportViewModel responseParentsResult;

            // TODO: usar polimorfismo aqui ou criar uma rota para cada report
            switch (level)
            {
                // TODO: Remover este case, vou disponibilizar apenas pais e filhos, este caso se aplicaria em caso de existência de avós
                case "ancestors":
                    var reportAncestorsResult = await _ancestorsReports.GetAncestorsReport(personFullName);
                    responseAncestorsResult = _mapper.Map<AncestorsReportViewModel>(reportAncestorsResult);
                    return Ok(responseAncestorsResult);

                case "children":
                    var reportChildrenResult = await _childrenReports.GetChildrenReport(personFullName);
                    responseChildrenResult = _mapper.Map<ChildrenReportViewModel>(reportChildrenResult);
                    return Ok(responseChildrenResult);

                case "parents":
                    var reportParentsResult = await _parentsReports.GetParentsReport(personFullName);
                    responseParentsResult = _mapper.Map<ParentsReportViewModel>(reportParentsResult);
                    return Ok(responseParentsResult);

                default:
                    return BadRequest(); // http - 400
            }
        }

        // GET api/researches/reports/get-filtered-report
        [HttpGet, Route("reports/filtered-report")]
        public async Task<ActionResult<IEnumerable<ResearchViewModel>>> GetFilteredReport([FromBody] FilterObjectViewModel modelFilter)
        {
            var filter = new Dictionary<string, string>
            {
                { "Region", modelFilter.Region },
                { "Name", modelFilter.Name },
                { "SkinColor", modelFilter.SkinColor },
                { "Schooling", modelFilter.Schooling }
            };

            var repositoryResponse = await _researchRepository.GetFilteredResearches(filter);
            var responseResult = _mapper.Map<IEnumerable<ResearchViewModel>>(repositoryResponse);

            return Ok(responseResult); // http - 200
        }
    }
}
