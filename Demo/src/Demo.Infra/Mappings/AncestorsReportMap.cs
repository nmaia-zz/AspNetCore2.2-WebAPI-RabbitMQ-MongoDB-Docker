using Demo.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Demo.Infra.Mappings
{
    public class AncestorsReportMap
    {
        public static void ConfigureMap()
        {
            BsonClassMap.RegisterClassMap<AncestorsReport>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapIdMember(x => x.Ancestors);
                map.MapIdMember(x => x.Parent);
            });
        }
    }
}
