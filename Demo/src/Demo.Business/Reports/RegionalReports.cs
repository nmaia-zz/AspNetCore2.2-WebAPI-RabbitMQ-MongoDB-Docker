using Demo.Business.Reports.Base;
using Demo.Contracts.Business;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class RegionalReports
        : BaseReports<RegionalReport>
        , IRegionalReports
    {
        private readonly IRepositoryResearch _researchRepository;

        public RegionalReports(IRepositoryResearch researchRepository)
            => _researchRepository = researchRepository;

        /// <summary>
        /// Retorna o percentual de Pessoas com mesmo nome de uma determinada região.
        /// </summary>
        /// <param name="researches"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, decimal>> GetPercentageByRegionReport(string region)
        {
            var allResearches = await _researchRepository.GetAll();

            var peopleFromRegion = allResearches.Where(
                x => x.Region.ToString() == region)
                    .Select(y => y.Person);

            var totalPeopleFromRegion = peopleFromRegion.Count();

            var groupResult = peopleFromRegion.GroupBy(x => x.Name)
                    .Select(group => 
                        new { 
                            Name = group.Key,
                            Percentage = getPercentage(group.Count(s => s.Name == group.Key), totalPeopleFromRegion) 
                        });

            var result = new Dictionary<string, decimal>();

            foreach (var item in groupResult)
                result.Add(item.Name, item.Percentage);

            return result;
        }

        public decimal getPercentage(int qttyPerName, int totalPeopleFromRegion)
        {
            var result = (decimal)qttyPerName / (decimal)totalPeopleFromRegion;
            return Decimal.Round(result,2);
        }            
    }
}
