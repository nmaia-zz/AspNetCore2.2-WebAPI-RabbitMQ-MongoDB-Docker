using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace Demo.Domain.Enums
{
    public enum Region
    {
        [Description("Northeast Region")]
        [BsonRepresentation(BsonType.String)]
        NORTHEST_REGION,

        [Description("North Region")]
        [BsonRepresentation(BsonType.String)]
        NORTH_REGION,

        [Description("Midwest Region")]
        [BsonRepresentation(BsonType.String)]
        MIDWEST_REGION,

        [Description("Southeast Region")]
        [BsonRepresentation(BsonType.String)]
        SOUTHEAST_REGION,

        [Description("South Region")]
        [BsonRepresentation(BsonType.String)]
        SOUTH_REGION
    }
}
