using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace Demo.Domain.Enums
{
    public enum Schooling
    {
        [Description("PhD")]
        [BsonRepresentation(BsonType.String)]
        PHD,

        [Description("Masters")]
        [BsonRepresentation(BsonType.String)]
        MASTERS,       

        [Description("Postgraduate")]
        [BsonRepresentation(BsonType.String)]
        POSTGRADUATE,

        [Description("University education")]
        [BsonRepresentation(BsonType.String)]
        UNIVERSITY_EDUCATION,

        [Description("Elementary school")]
        [BsonRepresentation(BsonType.String)]
        ELEMENTARY_SCHOOL,

        [Description("None")]
        [BsonRepresentation(BsonType.String)]
        NONE
    }
}
