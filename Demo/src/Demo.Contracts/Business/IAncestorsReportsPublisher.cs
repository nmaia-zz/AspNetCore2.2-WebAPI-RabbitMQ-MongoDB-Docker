using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IAncestorsReportsPublisher
    {
        AncestorsReport MountAncestorObjectToInsert(Research research);
        Task PublishToBeAddedIntoFamilyTree(Research research);
    }
}
