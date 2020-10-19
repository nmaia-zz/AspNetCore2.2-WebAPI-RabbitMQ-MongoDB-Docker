using Demo.Contracts.Database;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Demo.Infra.Data
{
    public class MongoDBContext : IMongoDBContext
    {
        public MongoClient _mongoClient { get; set; }
        private IMongoDatabase _database { get; set; }      

        private readonly IConfiguration _configuration;

        public MongoDBContext(IConfiguration configuration)
        {
            _configuration = configuration;    

            ConfigureMongo();
        }

        public IMongoCollection<T> GetCollection<T>(string name)
            => _database.GetCollection<T>(name);

        private void ConfigureMongo()
        {
            if (_mongoClient != null)
                return;

            _mongoClient = new MongoClient(_configuration.GetSection("MongoSettings").GetSection("Connection").Value);

            _database = _mongoClient.GetDatabase(_configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
