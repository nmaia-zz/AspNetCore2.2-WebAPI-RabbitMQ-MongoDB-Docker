using Demo.Contracts.Business;
using Demo.Contracts.Repository;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class FamilyTreeReports
        : IFamilyTreeReports
    {
        private readonly IAncestorsReportsRepository _ancestorsRepository;
        private readonly IChildrenReportsRepository _childrenRepository;
        private readonly IParentsReportsRepository _parentsRepository;

        public FamilyTreeReports(IAncestorsReportsRepository ancestorsRepository
            , IChildrenReportsRepository childrenRepository
            , IParentsReportsRepository parentsRepository)
        {
            _ancestorsRepository = ancestorsRepository;
            _childrenRepository = childrenRepository;
            _parentsRepository = parentsRepository;
        }

        public async Task<dynamic> GetFamilyTreeBasedOnLevelByPerson(string level, string personFullName)
        {
            switch (level.ToLower())
            {
                case "ancestors":
                    return await _ancestorsRepository.GetAncestorsByIdAsync(personFullName.ToLower());

                case "children":
                    return await _childrenRepository.GetChildrenByIdAsync(personFullName.ToLower());

                case "parents":
                    return await _parentsRepository.GetParentsByIdAsync(personFullName.ToLower());

                default:
                    return null;
            }
        }
    }
}
