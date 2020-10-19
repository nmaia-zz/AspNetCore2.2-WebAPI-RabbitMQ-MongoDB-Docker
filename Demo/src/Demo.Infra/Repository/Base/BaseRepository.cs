using Demo.Contracts.Database;
using Demo.Contracts.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Infra.Repository.Base
{
    public abstract class BaseRepository<TEntity> 
        : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly IMongoDBContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IMongoDBContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task Add(TEntity obj)
            => await DbSet.InsertOneAsync(obj);      

        public virtual async Task<TEntity> GetById(ObjectId id)
            => (await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id))).FirstOrDefault();

        public virtual async Task<IEnumerable<TEntity>> GetAll()
            => (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty)).ToList();

        public virtual async Task Update(TEntity obj)
            => await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj);

        public virtual async Task Remove(ObjectId id) 
            => await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));

        public void Dispose()
            => GC.SuppressFinalize(this);
    }
}
