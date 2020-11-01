using Demo.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Demo.Business.Contracts.QueuePublishers
{
    public interface IChildrenTreePublisher : IDisposable
    {
        ChildrenTree MountChildrenObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
