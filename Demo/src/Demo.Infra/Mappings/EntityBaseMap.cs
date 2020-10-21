using Demo.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Demo.Infra.Mappings
{
    public class EntityBaseMap
    {
        public static void ConfigureMap()
        {
            BsonClassMap.RegisterClassMap<EntityBase>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
            });
        }
    }
}
