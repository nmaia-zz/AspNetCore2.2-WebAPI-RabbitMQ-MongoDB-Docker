using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Business.Contracts.Services
{
    public interface IParentsTreeServices
    {
        Task<bool> PublishParentsFamilyTree(Research research);
    }
}
