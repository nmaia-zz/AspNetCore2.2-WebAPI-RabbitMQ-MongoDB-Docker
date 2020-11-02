using Demo.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Demo.Domain.Entities
{
    public class Person : EntityBase
    {
        #region ' Constructors '

        public Person()
        {
            Filiation = new string[2];
            Children = new List<string>();
        }

        public Person(string id, string firstName, string lastName, Gender gender, SkinColor skinColor, Schooling schooling, string[] filiation, List<string> children)
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

        public Person(string firstName, string lastName, Gender gender, SkinColor skinColor, Schooling schooling)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            SkinColor = skinColor;
            Schooling = schooling;
        }

        public Person(string firstName, string lastName, Gender gender, SkinColor skinColor, Schooling schooling, string[] filiation)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            SkinColor = skinColor;
            Schooling = schooling;
            Filiation = filiation;
        }

        public Person(string firstName, string lastName, Gender gender, SkinColor skinColor, Schooling schooling, List<string> children)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            SkinColor = skinColor;
            Schooling = schooling;
            Children = children;
        }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        } 

        #endregion

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Gender Gender { get; set; }

        [BsonRepresentation(BsonType.String)]
        public SkinColor SkinColor { get; set; }

        public string[] Filiation { get; set; }

        public IEnumerable<string> Children { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Schooling Schooling { get; set; }        
    }
}
