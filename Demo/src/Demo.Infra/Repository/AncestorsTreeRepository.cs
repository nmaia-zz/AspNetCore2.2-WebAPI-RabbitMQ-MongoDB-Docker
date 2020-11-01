using Demo.Domain.Entities;
using Demo.Infra.Contracts.MongoDB;
using Demo.Infra.Contracts.Repository;
using Demo.Infra.Repository.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class AncestorsTreeRepository
        : BaseRepository<AncestorsTree>
        , IAncestorsTreeRepository
    {
        public AncestorsTreeRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public virtual async Task<AncestorsTree> GetAncestorsByIdAsync(string id)
            => (await DbSet.FindAsync(Builders<AncestorsTree>.Filter.Eq("_id", id))).FirstOrDefault();
    }
}
