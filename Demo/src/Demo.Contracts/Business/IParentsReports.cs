using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IParentsReports
        : IReports<ParentsReport>
    {
        Task<IEnumerable<ParentsReport>> GetParentsReport();
    }
}
