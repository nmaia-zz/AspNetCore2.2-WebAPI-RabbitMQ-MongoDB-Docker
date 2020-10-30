using Bogus;
using Demo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using BOGUS = Bogus.DataSets;
using DOMAIN = Demo.Domain.Entities;

namespace Demo.UnitTests.Fixtures
{
    [CollectionDefinition(nameof(ResearchCollection))]
    public class ResearchCollection : ICollectionFixture<ResearchTestsFixture>{}

    public class ResearchTestsFixture : IDisposable
    {
        public DOMAIN.Research CreateValidResearch()
        {
            return CreateValidResearches(1, Region.SOUTHEAST_REGION, Schooling.UNIVERSITY_EDUCATION, SkinColor.BLACK).FirstOrDefault();            
        }

        public DOMAIN.Research CreateValidResearchesForWhitePeople()
        {
            return CreateValidResearches(10, Region.SOUTHEAST_REGION, Schooling.UNIVERSITY_EDUCATION, SkinColor.WHITE).FirstOrDefault();
        }

        public DOMAIN.Research CreateValidResearchesForBlackPeople()
        {
            return CreateValidResearches(10, Region.SOUTHEAST_REGION, Schooling.UNIVERSITY_EDUCATION, SkinColor.BLACK).FirstOrDefault();
        }

        public DOMAIN.Research CreateInvalidResearch()
        {
            var gender = new Faker().PickRandom<BOGUS.Name.Gender>();

            var research = new Faker<DOMAIN.Research>("en").CustomInstantiator(f => new DOMAIN.Research(

                    Region.SOUTHEAST_REGION,
                    new DOMAIN.Person(
                        f.Name.FirstName(gender),
                        f.Name.LastName(gender),
                        (Gender)Enum.Parse(typeof(Gender), gender.ToString().ToUpper()),
                        SkinColor.WHITE,
                        Schooling.UNIVERSITY_EDUCATION,
                        null, // Filiation
                        null) // Children
                ));

            return research;
        }

        public IEnumerable<DOMAIN.Research> CreateValidResearches(int qtty, Region region, Schooling schooling, SkinColor skinColor)
        {
            var gender = new Faker().PickRandom<BOGUS.Name.Gender>();

            var researches = new Faker<DOMAIN.Research>("en").CustomInstantiator(f => new DOMAIN.Research(

                    region,
                    new DOMAIN.Person(
                        f.Name.FirstName(gender),
                        f.Name.LastName(gender),
                        (Gender)Enum.Parse(typeof(Gender), gender.ToString().ToUpper()),
                        skinColor,
                        schooling,
                        new string[2]
                        {
                            string.Join(" ", f.Name.FirstName(BOGUS.Name.Gender.Male), f.Name.LastName(BOGUS.Name.Gender.Male)),
                            string.Join(" ", f.Name.FirstName(BOGUS.Name.Gender.Female), f.Name.LastName(BOGUS.Name.Gender.Female))
                        },
                        new List<string>() // Children
                        {
                            string.Join(" ", f.Name.FirstName(gender), f.Name.LastName(gender))
                        })
                ));

            return researches.Generate(qtty);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
