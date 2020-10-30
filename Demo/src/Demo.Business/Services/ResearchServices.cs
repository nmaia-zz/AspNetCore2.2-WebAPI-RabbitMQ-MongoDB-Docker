using Demo.Business.Contracts;
using Demo.Business.Models.Validations;
using Demo.Business.Services.Base;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.Contracts.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Services
{
    public class ResearchServices : BaseServices, IResearchServices
    {
        private readonly IResearchRepository _researchRepository;
        private readonly IQueueManagementResearch _queueManagementResearch;
        private readonly IAncestorsReportsPublisher _ancestorsReports;
        private readonly IChildrenReportsPublisher _childrenReports;
        private readonly IParentsReportsPublisher _parentsReports;

        public ResearchServices(INotifier notifier, 
                                IQueueManagementResearch queueManagementResearch, 
                                IResearchRepository researchRepository, 
                                IAncestorsReportsPublisher ancestorsReports, 
                                IChildrenReportsPublisher childrenReports, 
                                IParentsReportsPublisher parentsReports)
            : base(notifier)
        {
            _queueManagementResearch = queueManagementResearch;
            _researchRepository = researchRepository;
            _ancestorsReports = ancestorsReports;
            _childrenReports = childrenReports;
            _parentsReports = parentsReports;
        }

        public async Task<bool> PublishResearch(Research research)
        {
            if (!ExecuteValidation(new ResearchValidation(), research)
                || !ExecuteValidation(new PersonValidation(), research.Person))
                return false;

            var allResearches = await _researchRepository.GetAllAsync();
            
            var isDuplicatedResearch = 
                allResearches.Where(x => x.Person.FirstName.ToLower() == research.Person.FirstName.ToLower()
                && x.Person.LastName.ToLower() == research.Person.LastName.ToLower()).Any();

            if (isDuplicatedResearch)
            {
                Notify("This person has already answered the research.");
                return false;
            }

            await _queueManagementResearch.Publish(research, "researches.queue", "researches.exchange", "researches.queue*");
            return true;
        }

        public async Task<bool> PublishToParentsFamilyTree(Research research)
        {
            if (research.Person.Filiation.Any())
            {
                await _parentsReports.PublishToBeAddedIntoFamilyTree(research);
                return true;
            }

            return false;
        }

        public async Task<bool> PublishToChildrenFamilyTree(Research research)
        {
            if (research.Person.Children.Any())
            {
                await _childrenReports.PublishToBeAddedIntoFamilyTree(research);
                return true;
            }

            return false;                
        }

        public async Task<bool> PublishToAncestorsFamilyTree(Research research)
        {
            if (research.Person.Filiation.Any())
            {
                await _ancestorsReports.PublishToBeAddedIntoFamilyTree(research);
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _queueManagementResearch?.Dispose();
            _researchRepository?.Dispose();
            _ancestorsReports?.Dispose();
            _childrenReports?.Dispose();
            _parentsReports?.Dispose();
        }
    }
}
