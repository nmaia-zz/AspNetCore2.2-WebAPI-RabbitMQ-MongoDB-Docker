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
                new IgnoreIfDefaultConvention(true)
            };

            ConventionRegistry.Register("MyConventions", pack, t => true);
        }
    }
}
