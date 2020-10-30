using Demo.Domain.Entities;
using Demo.Infra.Contracts.MongoDB;
using Demo.Infra.Contracts.Repository;
using Demo.Infra.Repository.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class ParentsReportsRepository
        : BaseRepository<ParentsReport>
        , IParentsReportsRepository
    {
        public ParentsReportsRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public virtual async Task<ParentsReport> GetParentsByIdAsync(string id)
            => (await DbSet.FindAsync(Builders<ParentsReport>.Filter.Eq("_id", id))).FirstOrDefault();
    }
}
