using Demo.Contracts.Database;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class ChildrenReportsRepository
        : BaseRepository<ChildrenReport>
        , IChildrenReportsRepository
    {
        public ChildrenReportsRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public virtual async Task<ChildrenReport> GetChildrenByIdAsync(string id) 
            => (await DbSet.FindAsync(Builders<ChildrenReport>.Filter.Eq("_id", id))).FirstOrDefault();
    }
}
