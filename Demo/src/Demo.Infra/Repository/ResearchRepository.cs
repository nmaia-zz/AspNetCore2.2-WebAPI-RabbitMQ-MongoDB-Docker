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
    public class ResearchRepository 
        : BaseRepository<Research>
        , IResearchRepository
    {
        public ResearchRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public async Task<IEnumerable<Research>> GetFilteredResearches(FilterObject filter)
        {
            var predicate = PredicateBuilder.New<Research>();

            if (!string.IsNullOrEmpty(filter.Region))
                predicate = predicate.And(r => r.Region == (Region)Enum.Parse(typeof(Region), filter.Region.ToUpper()));

            if (!string.IsNullOrEmpty(filter.FirstName))
                predicate = predicate.And(r => r.Person.FirstName.ToLower().Contains(filter.FirstName.ToLower()));

            if (!string.IsNullOrEmpty(filter.Gender))
                predicate = predicate.And(r => r.Person.Gender == (Gender)Enum.Parse(typeof(Gender), filter.Gender.ToUpper()));

            if (!string.IsNullOrEmpty(filter.SkinColor))
                predicate = predicate.And(r => r.Person.SkinColor == (SkinColor)Enum.Parse(typeof(SkinColor), filter.SkinColor.ToUpper()));

            if (!string.IsNullOrEmpty(filter.Schooling))
                predicate = predicate.And(r => r.Person.Schooling == (Schooling)Enum.Parse(typeof(Schooling), filter.Schooling.ToUpper()));

            var queryResult = await Task.Run(() => {
                
                return DbSet.AsQueryable<Research>()
                            .Where(predicate).ToListAsync();
            });

            return queryResult;
        }

        public async Task<IEnumerable<FilteredResearchGrouped>> GetFilteredResearchesGrouped(FilterObject filter)
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
