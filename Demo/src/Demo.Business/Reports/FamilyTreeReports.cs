using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class FamilyTreeReports
        : BaseReports<FamilyTreeReport>
        , IFamilyTreeReports
    {
        public async Task<IEnumerable<FamilyTreeReport>> FamilyTreeReport(string level)
        {
            throw new System.NotImplementedException();
        }
    }
}
