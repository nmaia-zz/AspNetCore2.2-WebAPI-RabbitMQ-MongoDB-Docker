using Demo.Contracts.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Reports.Base
{
    public abstract class BaseReports<TEntity>
        : IReports<TEntity>
        where TEntity : class

    {
        public virtual async Task<IEnumerable<TEntity>> FilteredReport(string args)
        {
            throw new NotImplementedException();
        }
    }
}
