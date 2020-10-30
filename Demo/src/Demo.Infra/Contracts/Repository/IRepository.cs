using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.Repository
{
    public interface IRepository<TEntity> 
        : IDisposable 
        where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(ObjectId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity obj);
        Task RemoveAsync(ObjectId id);
    }
}
