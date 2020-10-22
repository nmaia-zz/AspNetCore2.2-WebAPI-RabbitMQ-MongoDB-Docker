using Demo.Contracts.Database;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Base;

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
    }
}
