using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IFamilyTreeReports
        : IReports<FamilyTreeReport>
    {
        Task<IEnumerable<FamilyTreeReport>> FamilyTreeReport(string level);
    }
}
