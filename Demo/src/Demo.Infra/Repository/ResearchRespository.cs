using Demo.Contracts.Database;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Demo.Domain.Enums;
using Demo.Infra.Repository.Base;
using LinqKit;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class ResearchRespository 
        : BaseRepository<Research>
        , IResearchRepository
    {
        public ResearchRespository(IMongoDBContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<Research>> GetFilteredResearches(Dictionary<string, string> filter)
        {
            var predicate = PredicateBuilder.New<Research>();

            // -------------------- region
            var region = (filter.Where(x => x.Key == "Region").Select(y => y.Value).FirstOrDefault() != string.Empty)
                ? filter.Where(x => x.Key == "Region").Select(y => y.Value).FirstOrDefault()
                : string.Empty;

            if (!string.IsNullOrEmpty(region))
                predicate = predicate.And(r => r.Region == (Region)Enum.Parse(typeof(Region), region.ToUpper()));

            // -------------------- name

            var firstName = (filter.Where(x => x.Key == "FirstName").Select(y => y.Value).FirstOrDefault() != string.Empty)
                ? filter.Where(x => x.Key == "FirstName").Select(y => y.Value).FirstOrDefault()
                : string.Empty;

            if (!string.IsNullOrEmpty(firstName))
                predicate = predicate.And(r => r.Person.FirstName.ToLower().Contains(firstName.ToLower()));

            // -------------------- gender

            var gender = (filter.Where(x => x.Key == "Gender").Select(y => y.Value).FirstOrDefault() != string.Empty)
                ? filter.Where(x => x.Key == "Gender").Select(y => y.Value).FirstOrDefault()
                : string.Empty;

            if (!string.IsNullOrEmpty(gender))
                predicate = predicate.And(r => r.Person.Gender == (Gender)Enum.Parse(typeof(Gender), gender.ToUpper()));

            // -------------------- skinColor

            var skinColor = (filter.Where(x => x.Key == "SkinColor").Select(y => y.Value).FirstOrDefault() != string.Empty)
                ? filter.Where(x => x.Key == "SkinColor").Select(y => y.Value).FirstOrDefault()
                : string.Empty;

            if (!string.IsNullOrEmpty(skinColor))
                predicate = predicate.And(r => r.Person.SkinColor == (SkinColor)Enum.Parse(typeof(SkinColor), skinColor.ToUpper()));

            // -------------------- schooling

            var schooling = (filter.Where(x => x.Key == "Schooling").Select(y => y.Value).FirstOrDefault() != string.Empty)
                ? filter.Where(x => x.Key == "Schooling").Select(y => y.Value).FirstOrDefault()
                : string.Empty;

            if (!string.IsNullOrEmpty(schooling))
                predicate = predicate.And(r => r.Person.Schooling == (Schooling)Enum.Parse(typeof(Schooling), schooling.ToUpper()));

            var queryResult = await Task.Run(() => {
                
                return DbSet.AsQueryable<Research>()
                            .Where(predicate).ToListAsync();
            });

            return queryResult;
        }

        public async Task<IEnumerable<FilteredResearchGrouped>> GetFilteredResearchesGrouped(Dictionary<string, string> filter)
        {
            var nonGroupedResponse = (await GetFilteredResearches(filter)).ToList();

            var groupedResponse = nonGroupedResponse.AsEnumerable()
                    .Select(x => new {

                        Region = x.Region.ToString(),
                        FirstName = x.Person.FirstName,
                        Gender = x.Person.Gender.ToString(),                        
                        SkinColor = x.Person.SkinColor.ToString(),                        
                        Schooling = x.Person.Schooling.ToString(),                       

                    }).GroupBy(g => new {

                        g.Region,
                        g.FirstName,
                        g.Gender,
                        g.Schooling,
                        g.SkinColor

                    }).Select(s => new FilteredResearchGrouped {

                        Region = s.Key.Region,
                        FirstName = s.Key.FirstName,
                        Gender = s.Key.Gender,
                        SkinColor = s.Key.SkinColor,
                        Schooling = s.Key.Schooling,
                        Quantity = Convert.ToInt16(s.Count()).ToString()

                    });

            return groupedResponse;
        }
    }
}
