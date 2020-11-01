using Demo.Business.Contracts;
using Demo.Business.Contracts.QueuePublishers;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using System;
using System.Threading.Tasks;

namespace Demo.Business.QueuePublishers
{
    public class ParentsTreePublisher : IParentsTreePublisher
    {
        private readonly IQueueManagerParentsTree _queueManagementParents;

        public ParentsTreePublisher(IQueueManagerParentsTree queueManagementParents)
        {
            _queueManagementParents = queueManagementParents;
        }

        public ParentsTree MountParentsObjectToInsert(Research research)
        {
            if (research.Person.Filiation[0] != null && research.Person.Filiation[1] != null)
            {
                var parents = new string[research.Person.Filiation.Length];
                var index = 0;

                foreach (var parent in research.Person.Filiation)
                {
                    parents[index] = parent;
                    index++;
                }

                var parentsObject = new ParentsTree()
                {
                    Id = string.Join(" ", research.Person.FirstName, research.Person.LastName),
                    Parent = string.Join(", ", parents[0], parents[1])
                };

                return parentsObject; 
            }

            return null;
        }

        public async Task PublishToBeAddedIntoFamilyTree(Research research)
        {
            var parents = MountParentsObjectToInsert(research);

            if (parents != null)
                await _queueManagementParents.Publish(parents, "parents.queue", "parents.exchange", "parents.queue*");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
