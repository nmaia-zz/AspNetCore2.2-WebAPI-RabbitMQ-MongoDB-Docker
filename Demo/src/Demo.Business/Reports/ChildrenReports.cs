using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class ChildrenReports
        : BaseReports<ChildrenReport>
        , IChildrenReports
    {
        public Task<IEnumerable<ChildrenReport>> GetChildrenReport()
        {
            throw new NotImplementedException();
        }
    }
}
