using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IRegionalReports
        : IReports<RegionalReport>
    {
        Task<double> PercentageReport(IEnumerable<Research> researches);
    }
}
