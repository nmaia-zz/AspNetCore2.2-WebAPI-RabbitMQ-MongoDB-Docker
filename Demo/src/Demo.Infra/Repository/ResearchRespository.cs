using Demo.Contracts.Database;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Demo.Infra.Repository.Base;

namespace Demo.Infra.Repository
{
    public class ResearchRespository 
        : BaseRepository<Research>
        , IResearchRepository
    {
        public ResearchRespository(IMongoDBContext context)
            : base(context)
        {

        }
    }
}
