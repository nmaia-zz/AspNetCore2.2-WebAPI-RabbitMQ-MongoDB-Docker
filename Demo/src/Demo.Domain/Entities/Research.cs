using Demo.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.Domain.Entities
{
    public class Research : EntityBase
    {
        #region ' Constructors '

        public Research()
        {

        }

        public Research(string id, Region region, Person person)
            : base(id)
        {
            Region = region;
            Person = person;
        } 

        #endregion

        [BsonRepresentation(BsonType.String)]
        public Region Region { get; set; }

        public Person Person { get; set; }
    }
}
