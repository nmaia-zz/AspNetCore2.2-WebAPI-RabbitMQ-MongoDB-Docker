using Demo.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Demo.Business.Contracts
{
    public interface IParentsReportsPublisher : IDisposable
    {
        ParentsReport MountParentsObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
