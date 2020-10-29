using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public abstract class EntityBase
    {
        public EntityBase()
        {

        }

        public EntityBase(string id)
        {
            Id = id;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
