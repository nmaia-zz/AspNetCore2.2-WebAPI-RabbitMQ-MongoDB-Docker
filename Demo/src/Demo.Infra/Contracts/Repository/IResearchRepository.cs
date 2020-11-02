using Demo.Domain.Entities;
using Demo.Infra.Contracts.Repository.Base;
using Demo.Infra.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.Repository
{
    public interface IResearchRepository 
        : IBaseRepository<Research>
    {
        Task<IEnumerable<Research>> GetFilteredResearches(FilterObjectDto filter);
        Task<IEnumerable<FilteredResearchGroupedDto>> GetFilteredResearchesGrouped(FilterObjectDto filter);
    }
}
