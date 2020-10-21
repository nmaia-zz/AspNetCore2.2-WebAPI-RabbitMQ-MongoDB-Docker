using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Enums
{
    public enum Gender
    {
        [BsonRepresentation(BsonType.String)]
        Male,

        [BsonRepresentation(BsonType.String)]
        Female
    }
}
