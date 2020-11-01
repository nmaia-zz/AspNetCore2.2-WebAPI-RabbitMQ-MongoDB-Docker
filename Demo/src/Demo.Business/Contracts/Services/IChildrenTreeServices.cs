using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Business.Contracts.Services
{
    public  interface IChildrenTreeServices
    {
        Task<bool> PublishChildrenFamilyTree(Research research);
    }
}
