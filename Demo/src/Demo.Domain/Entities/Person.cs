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

        public Person(string id, string firstName, string lastName, Gender gender, SkinColor skinColor, Schooling schooling, Person[] filiation, List<Person> children)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            SkinColor = skinColor;
            Schooling = schooling;
            Filiation = filiation;
            Children = children;
        }

        public Person(string id, string firstName, string lastName, Gender gender, SkinColor skinColor, Schooling schooling)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            SkinColor = skinColor;
            Schooling = schooling;
        }

        public Person(string id, string firstName, string lastName, Gender gender, SkinColor skinColor, Schooling schooling, Person[] filiation)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            SkinColor = skinColor;
            Schooling = schooling;
            Filiation = filiation;
        }

        public Person(string id, string firstName, string lastName, Gender gender, SkinColor skinColor, Schooling schooling, List<Person> children)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            SkinColor = skinColor;
            Schooling = schooling;
            Children = children;
        }

        public Person(string id, string firstName, string lastName)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
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
