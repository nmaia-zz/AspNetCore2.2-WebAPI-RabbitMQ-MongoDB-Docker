using Demo.API.Context;
using Demo.API.Models;
using Demo.API.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesquisasController : ControllerBase
    {
        // GET api/pesquisas/listar
        [Route("listar")]
        [HttpGet]
        public ActionResult<IEnumerable<Pesquisa>> Get()
        {
            var result = new MongoDBContext().ObterTodos<Pesquisa>();
            return Ok(result);
        }            

        // POST api/pesquisas/inserir
        [Route("inserir")]
        [HttpPost]
        public ActionResult<Pesquisa> Post([FromBody] Pesquisa pesquisa)
        {
            QueueManager queueManager = new QueueManager();
            queueManager.Publish(pesquisa);

            return Accepted(pesquisa);
        }

        [Route("integrar")]
        [HttpPost]
        public ActionResult<string> IntegrarPesquisas()
        {
            QueueManager queueManager = new QueueManager();
            queueManager.Subcribe("myqueue");

            return Ok("Pesquisas Integradas");
        }

    }
}
