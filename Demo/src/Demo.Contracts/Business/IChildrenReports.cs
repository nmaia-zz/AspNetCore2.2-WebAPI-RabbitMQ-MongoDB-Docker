using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IChildrenReports
        : IReports<ChildrenReport>
    {
        Task<IEnumerable<ChildrenReport>> GetChildrenReport();
    }
}
