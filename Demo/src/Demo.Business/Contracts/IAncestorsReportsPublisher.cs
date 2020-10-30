using Demo.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Demo.Business.Contracts
{
    public interface IAncestorsReportsPublisher : IDisposable
    {
        AncestorsReport MountAncestorObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
