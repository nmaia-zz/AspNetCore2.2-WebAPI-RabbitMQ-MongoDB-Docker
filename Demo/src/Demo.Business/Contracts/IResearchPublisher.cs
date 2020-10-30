using Demo.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Demo.Business.Contracts
{
    public interface IResearchServices : IDisposable
    {
        Task<bool> PublishResearch(Research research);
        Task<bool> PublishToParentsFamilyTree(Research research);
        Task<bool> PublishToChildrenFamilyTree(Research research);
        Task<bool> PublishToAncestorsFamilyTree(Research research);
    }
}
