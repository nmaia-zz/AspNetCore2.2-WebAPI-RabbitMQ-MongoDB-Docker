using Demo.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Demo.Infra.Mappings
{
    public static class ResearchMap
    {
        public static void ConfigureMap()
        {
            BsonClassMap.RegisterClassMap<EntityBase>(map => {

                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);

            });

            BsonClassMap.RegisterClassMap<Research>(map => {

                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Region);
                map.MapMember(x => x.Person);

            });

            BsonClassMap.RegisterClassMap<Person>(map => {

                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Name);
                map.MapMember(x => x.LastName);
                map.MapMember(x => x.Gender);
                map.MapMember(x => x.SkinColor);
                map.MapMember(x => x.Schooling);
                map.MapMember(x => x.Filiation);
                map.MapMember(x => x.Children);

            });
        }
    }
}
