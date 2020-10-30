using System.Threading.Tasks;

namespace Demo.Business.Contracts
{
    public interface IFamilyTreeReports
    {
        Task<dynamic> GetFamilyTreeBasedOnLevelByPerson(string level, string personFullName);
    }
}
