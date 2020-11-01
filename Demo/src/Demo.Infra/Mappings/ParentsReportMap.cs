using Demo.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Demo.Infra.Mappings
{
    public class ParentsReportMap
    {
        public static void ConfigureMap()
        {
            BsonClassMap.RegisterClassMap<ParentsTree>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Parent);
            });
        }
    }
}
