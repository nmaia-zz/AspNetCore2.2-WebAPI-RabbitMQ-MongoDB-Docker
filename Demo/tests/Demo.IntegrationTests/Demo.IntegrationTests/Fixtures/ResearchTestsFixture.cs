using Demo.Domain.Entities;
using Demo.Domain.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Xunit;

namespace Demo.Tests.Fixtures
{
    [CollectionDefinition(nameof(ResearchCollection))]
    public class ResearchCollection : ICollectionFixture<ResearchTestsFixture> { }

    public class ResearchTestsFixture : IDisposable
    {
        public Research CreateValidResearch()
        {
            var research = new Research()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Region = (Region)Enum.Parse(typeof(Region), "SOUTHEAST_REGION"),
                Person = new Person()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = "John",
                    LastName = "Highins",
                    Children = new List<Person>() {

                        new Person(){

                            Id = ObjectId.GenerateNewId().ToString(),
                            FirstName = "Bart",
                            LastName = "Highins",
                            Gender = Gender.MALE,
                            SkinColor = SkinColor.WHITE,
                            Schooling = Schooling.MASTERS
                        }
                    },
                    Filiation = new Person[2]{
                        new Person()
                        {
                            Id = ObjectId.GenerateNewId().ToString(),
                            FirstName = "Martin",
                            LastName = "Highins",
                            Gender = Gender.MALE,
                            SkinColor = SkinColor.WHITE,
                            Schooling = Schooling.MASTERS
                        },
                        new Person()
                        {
                            Id = ObjectId.GenerateNewId().ToString(),
                            FirstName = "Miranda",
                            LastName = "Highins",
                            Gender = Gender.FEMALE,
                            SkinColor = SkinColor.WHITE,
                            Schooling = Schooling.POSTGRADUATE
                        }
                    },
                    Gender = (Gender)Enum.Parse(typeof(Gender), "MALE"),
                    Schooling = (Schooling)Enum.Parse(typeof(Schooling), "MASTERS"),
                    SkinColor = (SkinColor)Enum.Parse(typeof(SkinColor), "WHITE")
                }

            };

            return research;
        }

        public Research CreateInvalidResearch()
        {
            var research = new Research()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Region = (Region)Enum.Parse(typeof(Region), "SOUTHEAST_REGION"),
                Person = new Person()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = "John",
                    LastName = "Highins",
                    Children = null,
                    Filiation = null,
                    Gender = (Gender)Enum.Parse(typeof(Gender), "MALE"),
                    Schooling = (Schooling)Enum.Parse(typeof(Schooling), "MASTERS"),
                    SkinColor = (SkinColor)Enum.Parse(typeof(SkinColor), "WHITE")
                }

            };

            return research;
        }

        public void Dispose()
        {

        }
    }
}
