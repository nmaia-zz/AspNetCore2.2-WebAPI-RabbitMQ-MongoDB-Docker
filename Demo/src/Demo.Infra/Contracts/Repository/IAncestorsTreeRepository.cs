using Demo.Domain.Entities;
using Demo.Infra.Contracts.Repository.Base;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.Repository
{
    public interface IAncestorsTreeRepository
        : IBaseRepository<AncestorsTree>
    {
        Task<AncestorsTree> GetAncestorsByIdAsync(string id);
    }
}
