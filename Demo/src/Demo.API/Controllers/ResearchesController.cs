using AutoMapper;
using Demo.API.ViewModels;
using Demo.Business.Contracts;
using Demo.Business.Contracts.Services;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    // TODO: Incluir o uso do polly para garantir a resiliencia da API
    // TODO: Criar conventions e configurar analyzers para as controllers da API
    // TODO: Criar tratamento de excecao
    [Route("api/researches")]
    [ApiController]
    public class ResearchesController : MainController
    {
        private readonly IResearchRepository _researchRepository;        
        private readonly IResearchServices _researchServices;
        private readonly IAncestorsTreeServices _ancestorsTreeServices;
        private readonly IChildrenTreeServices _childrenTreeServices;
        private readonly IParentsTreeServices _parentsTreeServices;
        private readonly IMapper _mapper;

        public ResearchesController(IResearchRepository researchRepository,
                                  IMapper mapper,
                                  IResearchServices researchServices,
                                  INotifier notifier, IAncestorsTreeServices ancestorsTreeServices, IChildrenTreeServices childrenTreeServices, IParentsTreeServices parentsTreeServices) : base(notifier)
        {
            _researchRepository = researchRepository;
            _mapper = mapper;
            _researchServices = researchServices;
            _ancestorsTreeServices = ancestorsTreeServices;
            _childrenTreeServices = childrenTreeServices;
            _parentsTreeServices = parentsTreeServices;
        }

        // GET api/researches/list-all
        [HttpGet("list-all")]
        public async Task<ActionResult<IEnumerable<ResearchViewModel>>> GetAllResearches()
        {
            var allResearches = await _researchRepository.GetAllAsync();

            if (allResearches == null || allResearches.Count() == 0)
                return NotFound();

            var model = _mapper.Map<IEnumerable<ResearchViewModel>>(allResearches);

            return CustomResponse(model);
        }

        // POST api/researches/insert-one
        [HttpPost("insert-one")]
        public async Task<ActionResult<ResearchViewModel>> InsertOneResearch([FromBody] ResearchViewModel model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            model.Id = ObjectId.GenerateNewId().ToString();
            model.Person.Id = ObjectId.GenerateNewId().ToString();

            var research = _mapper.Map<Research>(model);

            var researchHasBeenPublished = await _researchServices.PublishResearch(research);

            if (researchHasBeenPublished)
            {
                if(research.Person.Children.Any()) 
                    await _childrenTreeServices.PublishChildrenFamilyTree(research);
                
                await _parentsTreeServices.PublishParentsFamilyTree(research);
                await _ancestorsTreeServices.PublishAncestorsFamilyTree(research);
            }

            return CustomResponse(model);
        }
    }
}
