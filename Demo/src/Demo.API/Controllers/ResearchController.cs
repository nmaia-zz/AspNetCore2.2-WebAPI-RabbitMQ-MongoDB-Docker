using AutoMapper;
using Demo.API.DTO;
using Demo.Contracts.RabbitMQ;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [Route("api/researches")]
    [ApiController]
    public class ResearchController : ControllerBase
    {
        private readonly IQueueManagementResearch _queueManagementResearch;
        private readonly IRepositoryResearch _researchRepository;
        private readonly IMapper _mapper;

        public ResearchController(IRepositoryResearch researchRepository, IQueueManagementResearch queueMessageResearch, IMapper mapper)
        {
            _researchRepository = researchRepository;
            _queueManagementResearch = queueMessageResearch;
            _mapper = mapper;
        }

        // GET api/researches/list-all
        [HttpGet, Route("list-all")]
        public async Task<ActionResult<IEnumerable<PesquisaDto>>> GetAllResearches()
        {
            var response = await _researchRepository.GetAll();
            return Ok(response); // http - 200
        }

        // POST api/researches/inset-one
        [HttpPost, Route("insert-one")]
        public ActionResult<PesquisaDto> Post([FromBody] PesquisaDto research)
        {
            var item = _mapper.Map<Research>(research);

            _queueManagementResearch.Publish(item, "demo.queue", "demo.exchange", "demo.queue*");

            return Accepted(research); // http - 202
        }
    }
}
