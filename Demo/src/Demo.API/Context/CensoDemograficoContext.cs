using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Demo.API.Context
{
    public class CensoDemograficoContext
    {
        private IConfiguration _configuration;
        private IMongoDatabase _db;
        private MongoClient _client;

        public CensoDemograficoContext(IConfiguration configuration)
        {
            _configuration = configuration;

            _client = new MongoClient(_configuration.GetConnectionString("ConexaoDBCensoDemografico"));

            _db = _client.GetDatabase("DBCensoDemografico");
        }

        public IEnumerable<T> ObterTodos<T>()
        {
            return _db.GetCollection<T>("Pesquisas")
                .Find(_ => true).ToList();
        }

        public void InsereItem<T>(string document, T pesquisa)
        {            
            var collection = _db.GetCollection<T>(document);
            
            collection.InsertOne(pesquisa);
        }
    }
}
