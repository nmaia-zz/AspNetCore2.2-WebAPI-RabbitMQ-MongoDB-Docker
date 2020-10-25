using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Repository
{
    public interface IParentsReportsRepository
        : IRepository<ParentsReport>
    {
        Task<ParentsReport> GetParentsByIdAsync(string id);
    }
}
