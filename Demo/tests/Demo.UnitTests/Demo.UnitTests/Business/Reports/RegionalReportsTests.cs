using Demo.Business.Reports;
using Demo.Domain.Entities;
using Demo.Domain.Enums;
using Demo.Infra.Contracts.Repository;
using Demo.Tests.Fixtures;
using Demo.UnitTests.Fixtures;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests.Business.Reports
{
    [Collection(nameof(ResearchCollection))]
    public class RegionalReportsTests
    {
        private readonly ResearchTestsFixture _researchTestsFixture;
        private AutoMocker _mocker;

        public RegionalReportsTests(ResearchTestsFixture researchTestsFixture)
        {
            _mocker = new AutoMocker();
            _researchTestsFixture = researchTestsFixture;
        }

        [Theory(DisplayName = "Get Names Percentage by Region")]
        [Trait("Category", "Research Repository")]
        [InlineData("John", 0.5)]
        [InlineData("Albert", 0.2)]
        [InlineData("Brady", 0.3)]
        public void RegionalReport_Percentage_PeopleWithTheSameName(string firstName, decimal expectedResult)
        {
            // Arrange
            var researches_1 = _researchTestsFixture.CreateValidResearchesForMenWithTheSameName(5, "John", Region.SOUTH_REGION, Schooling.ELEMENTARY_SCHOOL, SkinColor.BLACK).ToList();
            var researches_2 = _researchTestsFixture.CreateValidResearchesForMenWithTheSameName(2, "Albert", Region.SOUTH_REGION, Schooling.ELEMENTARY_SCHOOL, SkinColor.BLACK).ToList();
            var researches_3 = _researchTestsFixture.CreateValidResearchesForMenWithTheSameName(3, "Brady", Region.SOUTH_REGION, Schooling.ELEMENTARY_SCHOOL, SkinColor.BLACK).ToList();

            var researches = new List<Research>();
            researches.AddRange(researches_1);
            researches.AddRange(researches_2);
            researches.AddRange(researches_3);

            var mock = _mocker.CreateInstance<RegionalReports>();

            var qttyPeopleWithSameName = researches.Count(x => x.Person.FirstName == firstName);
            var totalPeopleFromRegion = researches.Count();

            // Act
            var result = mock.GetPercentage(qttyPeopleWithSameName, totalPeopleFromRegion);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
