using Demo.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Demo.Business.Contracts.QueuePublishers
{
    public interface IAncestorsTreePublisher : IDisposable
    {
        AncestorsTree MountAncestorObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
