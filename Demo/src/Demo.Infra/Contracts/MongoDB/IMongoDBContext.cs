using MongoDB.Driver;
using System;

namespace Demo.Infra.Contracts.MongoDB
{
    public interface IMongoDBContext : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
