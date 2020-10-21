using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IRegionalReports
        : IReports<RegionalReport>
    {
        Task<Dictionary<string, decimal>> GetPercentageByRegionReport(string region);
    }
}
