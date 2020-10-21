using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.API.ViewModels
{
    public class EntityBaseViewModel
    {
        public EntityBaseViewModel()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
