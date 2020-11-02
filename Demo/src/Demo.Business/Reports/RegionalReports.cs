using Demo.Business.Contracts;
using Demo.Infra.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class RegionalReports : IRegionalReports
    {
        private readonly IResearchRepository _researchRepository;

        public RegionalReports(IResearchRepository researchRepository)
            => _researchRepository = researchRepository;

        /// <summary>
        /// Returns the percentage of people with the same name from specific region.
        /// </summary>
        /// <param name="researches"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, decimal>> GetNamesPercentageByRegion(string region)
        {
            var allResearches = await _researchRepository.GetAllAsync();

            var peopleFromRegion = allResearches.Where(
                x => x.Region.ToString().ToUpper() == region.ToUpper())
                    .Select(y => y.Person);

            var totalPeopleFromRegion = peopleFromRegion.Count();

            var groupResult = peopleFromRegion.GroupBy(x => x.FirstName)
                    .Select(group =>  new { 
                        Name = group.Key,
                        Percentage = GetPercentage(group.Count(s => s.FirstName == group.Key), totalPeopleFromRegion) 
                    });

            var result = new Dictionary<string, decimal>();

            foreach (var item in groupResult)
                result.Add(item.Name, item.Percentage);

            return result;
        }

        public decimal GetPercentage(int qttyPerName, int totalPeopleFromRegion)
        {
            var result = (decimal)qttyPerName / (decimal)totalPeopleFromRegion;
            return Decimal.Round(result,2);
        }            
    }
}
