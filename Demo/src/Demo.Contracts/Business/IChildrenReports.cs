using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IChildrenReports
        : IReports<ChildrenReport>
    {
        ChildrenReport MountChildrenObjectToInsert(Research research);
        Task<ChildrenReport> GetChildrenReport(string id);
    }
}
