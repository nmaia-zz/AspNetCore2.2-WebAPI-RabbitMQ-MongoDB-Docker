using Demo.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Demo.Domain.Entities
{
    public class Person : EntityBase
    {
        public Person()
            => this.Filiation = new Person[2];

        public string Name { get; set; }
        
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public SkinColor SkinColor { get; set; }

        public Person[] Filiation { get; set; }

        public IEnumerable<Person> Children { get; set; }

        public Schooling Schooling { get; set; }        
    }
}
