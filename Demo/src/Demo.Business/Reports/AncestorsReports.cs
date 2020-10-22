using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class AncestorsReports
        : BaseReports<AncestorsReport>
        , IAncestorsReports
    {
        public async Task<IEnumerable<AncestorsReport>> GetAncestorsReport()
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<AncestorsReport>> FilteredReport(string args)
        {
            return base.FilteredReport(args);
        }
    }
}
