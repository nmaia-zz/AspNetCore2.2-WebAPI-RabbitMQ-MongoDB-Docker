using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class ParentsReport
    {
        public ParentsReport()
        {

        }

        public ParentsReport(string id, string parent)
        {
            Id = id;
            Parent = parent;
        }

        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("parent")]
        public string Parent { get; set; }
    }
}
