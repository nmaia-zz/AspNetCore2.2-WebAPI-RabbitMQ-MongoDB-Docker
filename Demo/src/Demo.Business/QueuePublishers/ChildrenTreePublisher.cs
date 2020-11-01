using Demo.Business.Contracts;
using Demo.Business.Contracts.QueuePublishers;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.QueuePublishers
{
    public class ChildrenTreePublisher : IChildrenTreePublisher
    {
        private readonly IQueueManagerChildrenTree _queueManagerChildren;

        public ChildrenTreePublisher(IQueueManagerChildrenTree queueManagementChildren)
        {
            _queueManagerChildren = queueManagementChildren;
        }

        public ChildrenTree MountChildrenObjectToInsert(Research research)
        {
            if (research.Person.Children.Any())
            {
                var children = new string[research.Person.Children.Count()];
                var index = 0;

                foreach (var child in research.Person.Children)
                {
                    children[index] = child;
                    index++;
                }

                var childrenObject = new ChildrenTree()
                {
                    Id = string.Join(" ", research.Person.FirstName, research.Person.LastName),
                    Children = children
                };

                return childrenObject;
            }

            return null;
        }

        public async Task PublishToBeAddedIntoFamilyTree(Research research)
        {
            var children = MountChildrenObjectToInsert(research);

            if (children != null)
                await _queueManagerChildren.Publish(children, "children.queue", "children.exchange", "children.queue*");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
