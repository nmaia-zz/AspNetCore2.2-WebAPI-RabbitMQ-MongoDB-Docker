using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class ParentsReports
        : BaseReports<ParentsReport>
        , IParentsReports
    {
        public Task<IEnumerable<ParentsReport>> GetParentsReport()
        {
            throw new NotImplementedException();
        }
    }
}
