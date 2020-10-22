using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class ChildrenReports
        : BaseReports<ChildrenReport>
        , IChildrenReports
    {
        private readonly IChildrenRepository _childrenRepository;

        public ChildrenReports(IChildrenRepository childrenRepository)
            => _childrenRepository = childrenRepository;

        public ChildrenReport MountChildrenObjectToInsert(Research research)
        {
            var children = new string[research.Person.Children.Count()];
            var index = 0;

            foreach (var child in research.Person.Children)
            {
                children[index] = string.Join(" ", child.Name, child.LastName);
                index++;
            }

            var childrenObject = new ChildrenReport()
            {
                Id = string.Join(" ", research.Person.Name, research.Person.LastName),
                Children = children
            };

            return childrenObject;
        }

        public async Task<ChildrenReport> GetChildrenReport(string id)
        {
            return await _childrenRepository.GetChildrenById(id);
        }
    }
}
