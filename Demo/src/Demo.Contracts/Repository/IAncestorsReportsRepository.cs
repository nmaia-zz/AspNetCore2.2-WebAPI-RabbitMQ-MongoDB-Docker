using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Repository
{
    public interface IAncestorsReportsRepository
        : IRepository<AncestorsReport>
    {
        Task<AncestorsReport> GetAncestorsById(string id);
    }
}
