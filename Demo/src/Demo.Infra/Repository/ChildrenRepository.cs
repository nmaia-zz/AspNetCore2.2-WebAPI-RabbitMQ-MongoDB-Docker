using Demo.Contracts.Database;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class ChildrenRepository
        : BaseRepository<ChildrenReport>
        , IChildrenRepository
    {
        public ChildrenRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public virtual async Task<ChildrenReport> GetChildrenById(string id)
            => (await DbSet.FindAsync(Builders<ChildrenReport>.Filter.Eq("_id", id))).FirstOrDefault();
    }
}
