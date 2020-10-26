using Demo.Contracts.Business;
using Demo.Contracts.RabbitMQ;
using Demo.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class ParentsReportsPublisher : IParentsReportsPublisher
    {
        private readonly IQueueManagementParentsReport _queueManagementParents;

        public ParentsReportsPublisher(IQueueManagementParentsReport queueManagementParents)
        {
            _queueManagementParents = queueManagementParents;
        }

        public ParentsReport MountParentsObjectToInsert(Research research)
        {
            if (research.Person.Filiation[0] != null && research.Person.Filiation[1] != null)
            {
                var parents = new string[research.Person.Filiation.Length];
                var index = 0;

                foreach (var parent in research.Person.Filiation)
                {
                    parents[index] = string.Join(" ", parent.FirstName, parent.LastName);
                    index++;
                }

                var parentsObject = new ParentsReport()
                {
                    Id = string.Join(" ", research.Person.FirstName, research.Person.LastName),
                    Parent = string.Join(", ", parents[0], parents[1])
                };

                return parentsObject; 
            }

            return new ParentsReport();
        }

        public async Task PublishToBeAddedIntoFamilyTree(Research research)
        {
            var parents = MountParentsObjectToInsert(research);

            if (parents.Id != null && !string.IsNullOrEmpty(parents.Parent))
                await _queueManagementParents.Publish(parents, "parents.queue", "parents.exchange", "parents.queue*");
        }
    }
}
