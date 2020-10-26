using Demo.Domain.Entities;
using Demo.Domain.Enums;
using Demo.Infra.Data;
using Demo.Infra.Repository;
using FluentAssertions;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Demo.UnitTests.Business.Reports
{
    public class FamilyTreeReportsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("sarah bright")]
        //[InlineData("Sarah bright")]
        //[InlineData("sarah Bright")]
        //[InlineData("Sarah Bright")]
        //[InlineData("Sarah")]
        //[InlineData("Bright")]
        public async Task GetParentsByIdAsyncTest(string personFullName)
        {
            var config = Startup.InitConfiguration();
            var context = new MongoDBContext(config);

            var research = new Research()
            {

                Id = ObjectId.GenerateNewId().ToString(),
                Region = (Region)Enum.Parse(typeof(Region), "SOUTHEAST_REGION"),
                Person = new Person()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = "Leia",
                    LastName = "Skywalker",
                    Children = new List<Person>(),
                    Filiation = new Person[2] { 
                    
                        new Person()
                        {
                            FirstName = "Darth",
                            LastName = "Vader"
                        },
                        new Person()
                        {
                            FirstName = "Padme",
                            LastName = "Vader"
                        }

                    },
                    Gender = (Gender)Enum.Parse(typeof(Gender), "FEMALE"),
                    Schooling = (Schooling)Enum.Parse(typeof(Schooling), "MASTERS"),
                    SkinColor = (SkinColor)Enum.Parse(typeof(SkinColor), "WHITE")
                }

            };

            var researchRepository = new ResearchRespository(context);

            await researchRepository.AddAsync(research);

            ObjectId.TryParse(research.Id, out ObjectId id);
            var result = await researchRepository.GetByIdAsync(id);

            result.Id.Should().Be(research.Id, $"The research Id should be equals to: { research.Id }");
        }
    }
}
