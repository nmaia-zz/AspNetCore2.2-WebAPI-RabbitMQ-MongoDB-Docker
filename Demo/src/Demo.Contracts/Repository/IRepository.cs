using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Repository
{
    public interface IRepository<TEntity> 
        : IDisposable 
        where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(ObjectId id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity obj);
        Task Remove(ObjectId id);
    }
}
