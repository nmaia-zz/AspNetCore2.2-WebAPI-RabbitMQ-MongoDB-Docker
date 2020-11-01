using Demo.Business.Contracts;
using Demo.Business.Contracts.QueuePublishers;
using Demo.Business.Contracts.Services;
using Demo.Business.Services.Base;
using Demo.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Services
{
    public class ChildrenTreeServices : BaseServices, IChildrenTreeServices
    {
        private readonly IChildrenTreePublisher _childrenTreePublisher;

        public ChildrenTreeServices(INotifier notifier, IChildrenTreePublisher childrenTreePublisher)
            : base(notifier)
        {
            _childrenTreePublisher = childrenTreePublisher;
        }

        public async Task<bool> PublishChildrenFamilyTree(Research research)
        {
            if (research == null)
            {
                Notify("The research cannot be null.");
                return false;
            }

            if (research.Person.Children.Any())
            {
                await _childrenTreePublisher.PublishToBeAddedIntoFamilyTree(research);
                return true;
            }

            Notify("We've had an error when publishing 'children' family tree.");
            return false;
        }
    }
}
