using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.Repository
{
    public interface IAncestorsReportsRepository
        : IRepository<AncestorsReport>
    {
        Task<AncestorsReport> GetAncestorsByIdAsync(string id);
    }
}
