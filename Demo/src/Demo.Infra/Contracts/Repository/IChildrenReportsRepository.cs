using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.Repository
{
    public interface IChildrenReportsRepository
        : IRepository<ChildrenReport>
    {
        Task<ChildrenReport> GetChildrenByIdAsync(string id);
    }
}
