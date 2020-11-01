using Demo.Business.Contracts;
using Demo.Infra.Contracts.Repository;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class FamilyTreeReports
        : IFamilyTreeReports
    {
        private readonly IAncestorsTreeRepository _ancestorsRepository;
        private readonly IChildrenTreeRepository _childrenRepository;
        private readonly IParentsTreeRepository _parentsRepository;

        public FamilyTreeReports(IAncestorsTreeRepository ancestorsRepository
            , IChildrenTreeRepository childrenRepository
            , IParentsTreeRepository parentsRepository)
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
