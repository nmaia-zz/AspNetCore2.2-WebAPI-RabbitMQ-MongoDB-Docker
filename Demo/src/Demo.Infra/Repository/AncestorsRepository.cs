using Demo.Contracts.Database;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class AncestorsRepository
        : BaseRepository<AncestorsReport>
        , IAncestorsRepository
    {
        public AncestorsRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public virtual async Task<AncestorsReport> GetAncestorsById(string id)
            => (await DbSet.FindAsync(Builders<AncestorsReport>.Filter.Eq("_id", id))).FirstOrDefault();
    }
}
