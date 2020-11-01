using Demo.Domain.Entities;

namespace Demo.Business.Contracts.Services
{
    public interface IFilterObjectServices
    {
        bool IsValidFilter(Research filter);
    }
}
