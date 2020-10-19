using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IReports<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> FilteredReport(string args);
    }
}
