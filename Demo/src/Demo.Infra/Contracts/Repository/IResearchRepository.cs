using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.Repository
{
    public interface IResearchRepository 
        : IRepository<Research>
    {
        Task<IEnumerable<Research>> GetFilteredResearches(FilterObject filter);
        Task<IEnumerable<FilteredResearchGrouped>> GetFilteredResearchesGrouped(FilterObject filter);
    }
}
