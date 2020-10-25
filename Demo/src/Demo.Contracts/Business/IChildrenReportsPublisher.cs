using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IChildrenReportsPublisher
    {
        ChildrenReport MountChildrenObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
