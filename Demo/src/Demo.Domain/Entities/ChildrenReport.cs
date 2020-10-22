using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class ChildrenReport
    {
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("children")]
        public string[] Children { get; set; }
    }
}
