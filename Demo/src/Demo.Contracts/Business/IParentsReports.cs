using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IParentsReports
        : IReports<ParentsReport>
    {
        ParentsReport MountParentsObjectToInsert(Research research);
        Task<ParentsReport> GetParentsReport(string id);
    }
}
