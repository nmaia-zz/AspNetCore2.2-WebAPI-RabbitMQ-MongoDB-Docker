using Demo.API.Context;
using Demo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesquisasController : ControllerBase
    {
        private CensoDemograficoContext _mongoContext;

        public PesquisasController(CensoDemograficoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        // GET api/pesquisas
        [HttpGet]
        public ActionResult<IEnumerable<Pesquisa>> Get()
        {
            return Ok(_mongoContext.ObterTodos<Pesquisa>());
        }

        // POST api/pesquisas
        [HttpPost]
        public void Post([FromBody] Pesquisa pesquisa)
        {
            _mongoContext.InsereItem("Pesquisas", pesquisa);
        }
    }
}
