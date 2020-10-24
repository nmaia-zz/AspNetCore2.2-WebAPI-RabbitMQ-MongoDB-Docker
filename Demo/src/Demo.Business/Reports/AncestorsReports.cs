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
        private readonly IAncestorsReportsRepository _ancestorsRepository;

        public AncestorsReports(IAncestorsReportsRepository ancestorsRepository)
            => _ancestorsRepository = ancestorsRepository;

        public AncestorsReport MountAncestorObjectToInsert(Research research)
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

        public async Task<AncestorsReport> GetAncestorsReport(string id)
        {
            return await _ancestorsRepository.GetAncestorsById(id);
        }
    }
}
