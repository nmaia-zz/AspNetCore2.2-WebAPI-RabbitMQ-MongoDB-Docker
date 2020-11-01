using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Business.Contracts.Services
{
    public interface IResearchServices
    {
        Task<bool> PublishResearch(Research research);       
    }
}
