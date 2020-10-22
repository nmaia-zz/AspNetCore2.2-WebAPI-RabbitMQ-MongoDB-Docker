using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Repository
{
    public interface IChildrenRepository
        : IRepository<ChildrenReport>
    {
        Task<ChildrenReport> GetChildrenById(string id);
    }
}
