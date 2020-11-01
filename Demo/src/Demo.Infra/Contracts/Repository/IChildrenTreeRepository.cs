using Demo.Domain.Entities;
using Demo.Infra.Contracts.Repository.Base;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.Repository
{
    public interface IChildrenTreeRepository
        : IBaseRepository<ChildrenTree>
    {
        Task<ChildrenTree> GetChildrenByIdAsync(string id);
    }
}
