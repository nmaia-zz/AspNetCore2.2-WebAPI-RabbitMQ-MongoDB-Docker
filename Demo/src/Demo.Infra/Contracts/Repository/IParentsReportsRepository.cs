using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.Repository
{
    public interface IParentsReportsRepository
        : IRepository<ParentsReport>
    {
        Task<ParentsReport> GetParentsByIdAsync(string id);
    }
}
