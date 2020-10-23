using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Contracts.Repository
{
    public interface IResearchRepository 
        : IRepository<Research>
    {
        Task<IEnumerable<Research>> GetFilteredResearches(Dictionary<string,string> filter);
    }
}
