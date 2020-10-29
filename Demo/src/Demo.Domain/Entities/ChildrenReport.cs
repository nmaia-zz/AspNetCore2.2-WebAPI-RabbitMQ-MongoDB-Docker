using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class ChildrenReport
    {
        public ChildrenReport()
        {

        }

        public ChildrenReport(string id, string[] children)
        {
            Id = id;
            Children = children;
        }

        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("children")]
        public string[] Children { get; set; }
    }
}
