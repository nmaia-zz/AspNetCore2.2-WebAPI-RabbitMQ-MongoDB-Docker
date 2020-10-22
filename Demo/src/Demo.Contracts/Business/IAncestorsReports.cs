using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IAncestorsReports
        : IReports<AncestorsReport>
    {
        AncestorsReport MountAncestorObjectToInsert(Research research);
        Task<AncestorsReport> GetAncestorsReport(string id);        
    }
}
