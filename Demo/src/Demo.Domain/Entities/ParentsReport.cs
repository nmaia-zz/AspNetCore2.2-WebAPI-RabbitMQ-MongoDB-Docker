using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class ParentsReport
    {
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("parent")]
        public string Parent { get; set; }
    }
}
