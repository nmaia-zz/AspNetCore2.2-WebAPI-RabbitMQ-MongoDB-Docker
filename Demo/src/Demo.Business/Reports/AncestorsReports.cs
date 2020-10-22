using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class AncestorsReports
        : BaseReports<AncestorsReport>
        , IAncestorsReports
    {
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

        public async Task<IEnumerable<AncestorsReport>> GetAncestorsReport()
        {
            throw new System.NotImplementedException();
        }
    }
}
