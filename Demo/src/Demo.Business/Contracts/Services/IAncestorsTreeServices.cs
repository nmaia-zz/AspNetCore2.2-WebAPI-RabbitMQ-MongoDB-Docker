using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Business.Contracts.Services
{
    public interface IAncestorsTreeServices
    {
        Task<bool> PublishAncestorsFamilyTree(Research research);
    }
}
