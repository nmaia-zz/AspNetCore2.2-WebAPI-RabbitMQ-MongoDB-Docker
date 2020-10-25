using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IParentsReportsPublisher
    {
        ParentsReport MountParentsObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
