using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Demo.Infra.Mappings
{
    public static class MongoDBPersistence
    {
        public static void Setup()
        {
            ResearchMap.ConfigureMap();

            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true),
                new IgnoreIfNullConvention(true),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("MyConventions", pack, t => true);
        }
    }
}
