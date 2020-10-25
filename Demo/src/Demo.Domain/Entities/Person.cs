using Demo.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Demo.Domain.Entities
{
    public class Person : EntityBase
    {
        public Person()
        {
            Filiation = new Person[2];
            Children = new List<Person>();
        }            

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Gender Gender { get; set; }

        [BsonRepresentation(BsonType.String)]
        public SkinColor SkinColor { get; set; }

        public Person[] Filiation { get; set; }

        public IEnumerable<Person> Children { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Schooling Schooling { get; set; }        
    }
}
