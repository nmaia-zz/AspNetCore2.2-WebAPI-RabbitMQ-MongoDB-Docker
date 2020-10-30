using Demo.Domain.Entities;

namespace Demo.Business.Contracts
{
    public interface IFilterObjectServices
    {
        bool IsValidFilter(FilterObject filter);
    }
}
