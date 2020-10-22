using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IAncestorsReports
        : IReports<AncestorsReport>
    {
        Task<IEnumerable<AncestorsReport>> GetAncestorsReport();
    }
}
