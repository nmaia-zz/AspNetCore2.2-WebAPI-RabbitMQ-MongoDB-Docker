using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Demo.API.Context
{
    public class MongoDBContext
    {
        private IMongoDatabase _db;
        private MongoClient _client;

        public MongoDBContext()
        {
            _client = new MongoClient("mongodb://root:root123@mongo:27017");

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
