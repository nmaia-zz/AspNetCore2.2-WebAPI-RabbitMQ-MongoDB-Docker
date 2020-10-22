using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Repository
{
    public interface IAncestorsRepository
        : IRepository<AncestorsReport>
    {
        Task<AncestorsReport> GetAncestorsById(string id);
    }
}
