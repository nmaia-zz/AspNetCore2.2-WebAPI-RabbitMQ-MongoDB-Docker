using Demo.Domain.Entities;
using Demo.Infra.Contracts.MongoDB;
using Demo.Infra.Contracts.Repository;
using Demo.Infra.Repository.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class ChildrenTreeRepository
        : BaseRepository<ChildrenTree>
        , IChildrenTreeRepository
    {
        public ChildrenTreeRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public virtual async Task<ChildrenTree> GetChildrenByIdAsync(string id) 
            => (await DbSet.FindAsync(Builders<ChildrenTree>.Filter.Eq("_id", id))).FirstOrDefault();
    }
}
