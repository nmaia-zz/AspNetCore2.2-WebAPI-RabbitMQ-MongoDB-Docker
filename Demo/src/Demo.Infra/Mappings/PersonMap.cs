using Demo.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Demo.Infra.Mappings
{
    public class PersonMap
    {
        public static void ConfigureMap()
        {
            BsonClassMap.RegisterClassMap<Person>(map => {

                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.FirstName);
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
