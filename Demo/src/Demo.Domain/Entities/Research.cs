using Demo.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class Research : EntityBase
    {
        public Region Region { get; set; }

        public Person Person { get; set; }
    }
}
