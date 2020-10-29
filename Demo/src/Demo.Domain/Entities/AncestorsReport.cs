using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class AncestorsReport
    {
        public AncestorsReport()
        {

        }

        public AncestorsReport(string id, string[] ancestors, string parent)
        {
            Id = id;
            Ancestors = ancestors;
            Parent = parent;            
        }

        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("ancestors")]
        public string[] Ancestors { get; set; }

        [BsonElement("parent")]
        public string Parent { get; set; }
    }
}
