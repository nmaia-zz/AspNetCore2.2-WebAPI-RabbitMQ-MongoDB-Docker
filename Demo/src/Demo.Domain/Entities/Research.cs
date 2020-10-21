using Demo.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class Research : EntityBase
    {
        [BsonRepresentation(BsonType.String)]
        public Region Region { get; set; }

        public Person Person { get; set; }
    }
}
