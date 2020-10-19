using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class RegionalReports
        : BaseReports<RegionalReport>
        , IRegionalReports
    {
        public async Task<double> PercentageReport(IEnumerable<Research> researches)
        {
            throw new System.NotImplementedException();
        }
    }
}
