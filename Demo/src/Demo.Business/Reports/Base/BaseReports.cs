using Demo.Contracts.Business;

namespace Demo.Business.Reports.Base
{
    public abstract class BaseReports<TEntity>
        : IReports<TEntity>
        where TEntity : class
    {

    }
}
