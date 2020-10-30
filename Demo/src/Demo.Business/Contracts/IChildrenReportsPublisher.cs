using Demo.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Demo.Business.Contracts
{
    public interface IChildrenReportsPublisher : IDisposable
    {
        ChildrenReport MountChildrenObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
