using Demo.Contracts.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Demo.Infra.Data
{
    public class MongoDBContext : IMongoDBContext
    {
        public MongoClient _mongoClient { get; set; }
        private IMongoDatabase _database { get; set; }

        //private readonly IConfiguration _configuration;
        private readonly IOptions<MongoDBSettings> _configuration;

        public MongoDBContext(IOptions<MongoDBSettings> configuration)
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

            //_mongoClient = new MongoClient(_configuration.GetSection("MongoSettings").GetSection("Connection").Value);
            //_database = _mongoClient.GetDatabase(_configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);
            _mongoClient = new MongoClient(_configuration.Value.Connection);
            _database = _mongoClient.GetDatabase(_configuration.Value.DatabaseName);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
