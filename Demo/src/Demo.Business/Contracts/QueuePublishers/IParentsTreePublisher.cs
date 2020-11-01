using Demo.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Demo.Business.Contracts.QueuePublishers
{
    public interface IParentsTreePublisher : IDisposable
    {
        ParentsTree MountParentsObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
