using Demo.Contracts.Database;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Base;

namespace Demo.Infra.Repository
{
    public class RespositoryResearch 
        : BaseRepository<Research>
        , IRepositoryResearch
    {
        public RespositoryResearch(IMongoDBContext context)
            : base(context)
        {

        }
    }
}
