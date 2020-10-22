using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class AncestorsReport : Report
    {
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("ancestors")]
        public string[] Ancestors { get; set; }

        [BsonElement("parent")]
        public string Parent { get; set; }
    }
}
