using Demo.Infra.Contracts.MongoDB;
using Demo.Infra.Contracts.Repository.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Infra.Repository.Base
{
    public abstract class BaseRepository<TEntity>
        : IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        protected readonly IMongoDBContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IMongoDBContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task AddAsync(TEntity obj)
            => await DbSet.InsertOneAsync(obj);

        public virtual async Task<TEntity> GetByIdAsync(ObjectId id)
            => (await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id))).FirstOrDefault();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
            => (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty)).ToList();            

        public virtual async Task UpdateAsync(TEntity obj)
            => await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj);

        public virtual async Task RemoveAsync(ObjectId id)
            => await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));

        public void Dispose()
            => GC.SuppressFinalize(this);
    }
}
