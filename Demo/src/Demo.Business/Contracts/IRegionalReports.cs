using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Contracts
{
    public interface IRegionalReports
    {
        Task<Dictionary<string, decimal>> GetNamesPercentageByRegion(string region);
    }
}
