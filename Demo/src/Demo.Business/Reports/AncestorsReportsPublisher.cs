using Demo.Contracts.Business;
using Demo.Contracts.RabbitMQ;
using Demo.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class AncestorsReportsPublisher : IAncestorsReportsPublisher
    {
        private readonly IQueueManagementAncestorsReport _queueManagementAncestors;

        public AncestorsReportsPublisher(IQueueManagementAncestorsReport queueManagementAncestors)
        { 
            _queueManagementAncestors = queueManagementAncestors;
        }

        public AncestorsReport MountAncestorObjectToInsert(Research research)
        {
            if (research.Person.Filiation[0] != null && research.Person.Filiation[1] != null)
            {
                var ancestors = new string[research.Person.Filiation.Length];
                var index = 0;

                foreach (var filiation in research.Person.Filiation)
                {
                    ancestors[index] = string.Join(" ", filiation.FirstName, filiation.LastName);
                    index++;
                }

                var ancestorObject = new AncestorsReport()
                {
                    Id = string.Join(" ", research.Person.FirstName, research.Person.LastName),
                    Ancestors = ancestors,
                    Parent = string.Join(", ", research.Person.Filiation[0].FirstName, research.Person.Filiation[1].FirstName)
                };

                return ancestorObject; 
            }

            return new AncestorsReport();
        }

        public async Task PublishToBeAddedIntoFamilyTree(Research research)
        {
            var ancestors = MountAncestorObjectToInsert(research);

            if (ancestors.Id != null && !string.IsNullOrEmpty(ancestors.Parent))
                await _queueManagementAncestors.Publish(ancestors, "ancestors.queue", "ancestors.exchange", "ancestors.queue*");
        }
    }
}
