using MongoDB.Driver;
using System;

namespace Demo.Contracts.Database
{
    public interface IMongoDBContext : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
