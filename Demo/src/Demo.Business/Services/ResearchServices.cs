using Demo.Business.Contracts;
using Demo.Business.Contracts.Services;
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
        private readonly IQueueManagerResearch _queueManagerResearch;        

        public ResearchServices(INotifier notifier, 
                                IQueueManagerResearch queueManagementResearch, 
                                IResearchRepository researchRepository)
            : base(notifier)
        {
            _queueManagerResearch = queueManagementResearch;
            _researchRepository = researchRepository;
        }

        public async Task<bool> PublishResearch(Research research)
        {
            if (research == null)
            {
                Notify("The research cannot be null.");
                return false;
            }

            if (!ExecuteValidation(new ResearchValidation(), research)
                || !ExecuteValidation(new PersonValidation(), research.Person))
                return false;

            var allResearches = await _researchRepository.GetAllAsync();
            
            var isDuplicatedResearch = 
                allResearches.Where(x => x.Person.FirstName.ToLower() == research.Person.FirstName.ToLower()
                && x.Person.LastName.ToLower() == research.Person.LastName.ToLower()).Any();

            if (isDuplicatedResearch)
            {
                Notify("This person has answered the research already.");
                return false;
            }              

            await _queueManagerResearch.Publish(research, "researches.queue", "researches.exchange", "researches.queue*");
            return true;
        }
    }
}
