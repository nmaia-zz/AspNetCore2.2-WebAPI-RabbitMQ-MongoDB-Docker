using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Business.Contracts
{
    public interface IRegionalReports
    {
        Task<RegionalReport> GetPercentageByRegionReport(string region);
    }
}
