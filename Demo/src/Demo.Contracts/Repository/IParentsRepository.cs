using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Repository
{
    public interface IParentsRepository
        : IRepository<ParentsReport>
    {
        Task<ParentsReport> GetParentsById(string id);
    }
}
