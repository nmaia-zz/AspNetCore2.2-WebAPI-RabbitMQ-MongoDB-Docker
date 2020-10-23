using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Repository
{
    public interface IChildrenReportsRepository
        : IRepository<ChildrenReport>
    {
        Task<ChildrenReport> GetChildrenById(string id);
    }
}
