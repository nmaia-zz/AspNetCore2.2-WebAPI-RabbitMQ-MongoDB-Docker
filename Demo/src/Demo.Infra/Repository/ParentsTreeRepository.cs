using Demo.Domain.Entities;
using Demo.Infra.Contracts.MongoDB;
using Demo.Infra.Contracts.Repository;
using Demo.Infra.Repository.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class ParentsTreeRepository
        : BaseRepository<ParentsTree>
        , IParentsTreeRepository
    {
        public ParentsTreeRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public virtual async Task<ParentsTree> GetParentsByIdAsync(string id)
            => (await DbSet.FindAsync(Builders<ParentsTree>.Filter.Eq("_id", id))).FirstOrDefault();
    }
}
