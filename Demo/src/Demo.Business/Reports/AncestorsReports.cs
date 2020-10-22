using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class AncestorsReports
        : BaseReports<AncestorsReport>
        , IAncestorsReports
    {
        private readonly IAncestorsRepository _ancestorsRepository;

        public AncestorsReports(IAncestorsRepository ancestorsRepository)
            => _ancestorsRepository = ancestorsRepository;

        public AncestorsReport MountAncestorObjectToInsert(Research research)
        {
            var ancestors = new string[research.Person.Filiation.Length];
            var index = 0;

            foreach (var filiation in research.Person.Filiation)
            {
                ancestors[index] = string.Join(" ", filiation.Name, filiation.LastName);
                index++;
            }

            var ancestorObject = new AncestorsReport()
            {
                Id = string.Join(" ", research.Person.Name, research.Person.LastName),
                Ancestors = ancestors,
                Parent = string.Join(", ", research.Person.Filiation[0].Name, research.Person.Filiation[1].Name)
            };
            
            return ancestorObject;
        }

        public async Task<AncestorsReport> GetAncestorsReport(string id)
        {
            return await _ancestorsRepository.GetAncestorsById(id);
        }
    }
}
