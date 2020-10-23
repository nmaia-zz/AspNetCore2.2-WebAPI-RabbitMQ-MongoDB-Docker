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
                predicate = predicate.Or(r => r.Region == (Region)Enum.Parse(typeof(Region), region));

            // -------------------- name

            var name = (filter.Where(x => x.Key == "Name").Select(y => y.Value).FirstOrDefault() != string.Empty)
                ? filter.Where(x => x.Key == "Name").Select(y => y.Value).FirstOrDefault()
                : string.Empty;

            if (!string.IsNullOrEmpty(name))
                predicate = predicate.Or(r => r.Person.Name.ToLower().Contains(name.ToLower()));

            // -------------------- skinColor

            var skinColor = (filter.Where(x => x.Key == "SkinColor").Select(y => y.Value).FirstOrDefault() != string.Empty)
                ? filter.Where(x => x.Key == "SkinColor").Select(y => y.Value).FirstOrDefault()
                : string.Empty;

            if (!string.IsNullOrEmpty(skinColor))
                predicate = predicate.Or(r => r.Person.SkinColor == (SkinColor)Enum.Parse(typeof(SkinColor), skinColor));

            // -------------------- schooling

            var schooling = (filter.Where(x => x.Key == "Schooling").Select(y => y.Value).FirstOrDefault() != string.Empty)
                ? filter.Where(x => x.Key == "Schooling").Select(y => y.Value).FirstOrDefault()
                : string.Empty;

            if (!string.IsNullOrEmpty(schooling))
                predicate = predicate.Or(r => r.Person.Schooling == (Schooling)Enum.Parse(typeof(Schooling), schooling));

            var queryResult = await Task.Run(() => {
                
                return DbSet.AsQueryable<Research>()
                            .Where(predicate).ToListAsync();
            });

            return queryResult;
        }
    }
}
