using Demo.Business.Contracts;
using Demo.Business.Contracts.QueuePublishers;
using Demo.Business.Contracts.Services;
using Demo.Business.Services.Base;
using Demo.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Services
{
    public class ParentsTreeServices : BaseServices, IParentsTreeServices
    {
        private readonly IParentsTreePublisher _parentsTreePublisher;

        public ParentsTreeServices(INotifier notifier, IParentsTreePublisher parentsTreePublisher)
            : base(notifier)
        {
            _parentsTreePublisher = parentsTreePublisher;
        }

        public async Task<bool> PublishParentsFamilyTree(Research research)
        {
            if (research == null)
            {
                Notify("The research cannot be null.");
                return false;
            }

            if (research.Person.Filiation.Any())
            {
                await _parentsTreePublisher.PublishToBeAddedIntoFamilyTree(research);
                return true;
            }

            Notify("We've had an error when publishing 'parents' family tree.");
            return false;
        }
    }
}
