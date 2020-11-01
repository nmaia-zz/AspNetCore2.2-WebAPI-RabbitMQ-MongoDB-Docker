using Demo.Business.Contracts.QueuePublishers;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using System;
using System.Threading.Tasks;

namespace Demo.Business.QueuePublishers
{
    public class AncestorsTreePublisher : IAncestorsTreePublisher
    {
        private readonly IQueueManagerAncestorsTree _queueManagementAncestors;

        public AncestorsTreePublisher(IQueueManagerAncestorsTree queueManagementAncestors)
        { 
            _queueManagementAncestors = queueManagementAncestors;
        }

        public AncestorsTree MountAncestorObjectToInsert(Research research)
        {
            if (research.Person.Filiation[0] != null && research.Person.Filiation[1] != null)
            {
                var ancestors = new string[research.Person.Filiation.Length];
                var index = 0;

                foreach (var filiation in research.Person.Filiation)
                {
                    ancestors[index] = filiation;
                    index++;
                }

                var ancestorObject = new AncestorsTree()
                {
                    Id = string.Join(" ", research.Person.FirstName, research.Person.LastName),
                    Ancestors = ancestors,
                    Parent = string.Join(", ", research.Person.Filiation[0], research.Person.Filiation[1])
                };

                return ancestorObject; 
            }

            return null;
        }

        public async Task PublishToBeAddedIntoFamilyTree(Research research)
        {
            var ancestors = MountAncestorObjectToInsert(research);

            if (ancestors != null)
                await _queueManagementAncestors.Publish(ancestors, "ancestors.queue", "ancestors.exchange", "ancestors.queue*");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
