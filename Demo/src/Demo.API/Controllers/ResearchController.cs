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
    // TODO: Incluir o uso do polly para garantir a resiliencia da API
    // TODO: Configurar o swagger + comentarios para documentacao apropriada da API
    // TODO: Criar convencions e configurar analyzers para as controllers da API
    // TODO: Criar tratamento de excecao
    [EnableCors("AllowAnyOrigin")]
    [Route("api/researches")]
    [ApiController]
    public class ResearchController : MainController
    {
        private readonly IResearchRepository _researchRepository;
        private readonly IMapper _mapper;
        private readonly IResearchServices _researchServices;

        public ResearchController(IResearchRepository researchRepository, 
                                  IMapper mapper, 
                                  IResearchServices researchServices,
                                  INotifier notifier) : base(notifier)
        {
            _researchRepository = researchRepository;
            _mapper = mapper;
            _researchServices = researchServices;
        }

        // GET api/researches/list-all
        [HttpGet, Route("list-all")]
        public async Task<ActionResult<IEnumerable<ResearchViewModel>>> GetAllResearches()
        {
            using (_researchRepository)
            {
                var allResearches = await _researchRepository.GetAllAsync();

                if (allResearches == null || allResearches.Count() == 0)
                    return NotFound();

                var model = _mapper.Map<IEnumerable<ResearchViewModel>>(allResearches);

                return CustomResponse(model); 
            }
        }

        // POST api/researches/insert-one
        [HttpPost, Route("insert-one")]
        public async Task<ActionResult<ResearchViewModel>> InsertOneResearch([FromBody] ResearchViewModel model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);            

            using (_researchServices)
            {
                var research = _mapper.Map<Research>(model);

                await _researchServices.PublishResearch(research);
                await _researchServices.PublishToChildrenFamilyTree(research);
                await _researchServices.PublishToParentsFamilyTree(research);
                await _researchServices.PublishToAncestorsFamilyTree(research);
                
                return CustomResponse(model);
            }
        }
    }
}
