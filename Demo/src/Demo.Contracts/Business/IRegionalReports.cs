using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IRegionalReports
    {
        Task<RegionalReport> GetPercentageByRegionReport(string region);
    }
}
