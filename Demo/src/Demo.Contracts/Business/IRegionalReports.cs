using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IRegionalReports
        : IReports<RegionalReport>
    {
        Task<RegionalReport> GetPercentageByRegionReport(string region);
    }
}
