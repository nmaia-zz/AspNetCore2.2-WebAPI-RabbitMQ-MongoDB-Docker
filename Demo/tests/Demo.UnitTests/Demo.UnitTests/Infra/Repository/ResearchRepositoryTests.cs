using Demo.Domain.Enums;
using Demo.Infra.Contracts.Repository;
using Demo.UnitTests.Fixtures;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests.Infra.Repository
{
    [Collection(nameof(ResearchCollection))]
    public class ResearchRepositoryTests
    {
        private readonly ResearchTestsFixture _researchTestsFixture;

        public ResearchRepositoryTests(ResearchTestsFixture researchTestsFixture)
        {
            _researchTestsFixture = researchTestsFixture;
        }

        [Fact(DisplayName = "Get All Researches")]
        [Trait("Category", "Research Repository")]
        public async Task ResearchRepository_GetAllAsync_Returning15Rows()
        {
            // Arrange
            var researches = _researchTestsFixture.CreateValidResearches(15, Region.NORTH_REGION, Schooling.UNIVERSITY_EDUCATION, SkinColor.BROWN);
            var iResearchRepository = new Mock<IResearchRepository>();

            iResearchRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(researches);

            // Act
            var result = await iResearchRepository.Object.GetAllAsync();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(15, "There are 15 researches in the database.");
        }
    }
}
