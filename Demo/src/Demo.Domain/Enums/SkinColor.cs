using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace Demo.Domain.Enums
{
    public enum SkinColor
    {
        [Description("Albino")]
        [BsonRepresentation(BsonType.String)]
        ALBINO,

        [Description("White")]
        [BsonRepresentation(BsonType.String)]
        WHITE,

        [Description("Yellow")]
        [BsonRepresentation(BsonType.String)]
        YELLOW,

        [Description("Olive")]
        [BsonRepresentation(BsonType.String)]
        OLIVE,

        [Description("Brown")]
        [BsonRepresentation(BsonType.String)]
        BROWN,

        [Description("Black")]
        [BsonRepresentation(BsonType.String)]
        BLACK,

        [Description("Burnt")]
        [BsonRepresentation(BsonType.String)]
        BURNT
    }
}
