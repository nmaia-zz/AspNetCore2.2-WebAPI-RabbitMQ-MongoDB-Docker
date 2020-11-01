using Demo.Business.Contracts;
using Demo.Business.Contracts.QueuePublishers;
using Demo.Business.Contracts.Services;
using Demo.Business.Services.Base;
using Demo.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Services
{
    public class AncestorsTreeServices : BaseServices, IAncestorsTreeServices
    {
        private readonly IAncestorsTreePublisher _ancestorsTreePublisher;

        public AncestorsTreeServices(INotifier notifier, IAncestorsTreePublisher ancestorsTreePublisher)
            : base(notifier)
        {
            _ancestorsTreePublisher = ancestorsTreePublisher;
        }

        public async Task<bool> PublishAncestorsFamilyTree(Research research)
        {
            if (research == null)
            {
                Notify("The research cannot be null.");
                return false;
            }

            if (research.Person.Filiation.Any())
            {
                await _ancestorsTreePublisher.PublishToBeAddedIntoFamilyTree(research);
                return true;
            }

            Notify("We've had an error when publishing 'ancestors' family tree.");
            return false;
        }
    }
}
