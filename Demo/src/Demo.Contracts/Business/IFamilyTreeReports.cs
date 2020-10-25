using System.Threading.Tasks;

namespace Demo.Contracts.Business
{
    public interface IFamilyTreeReports
    {
        Task<dynamic> GetFamilyTreeBasedOnLevelByPerson(string level, string personFullName);
    }
}
