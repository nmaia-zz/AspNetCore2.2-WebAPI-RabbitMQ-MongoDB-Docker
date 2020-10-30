using Demo.Business.Reports;
using Demo.Infra.Contracts.Repository;
using Demo.Tests.Fixtures;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Demo.UnitTests.Business.Reports
{
    [Collection(nameof(ReportsCollection))]
    public class FamilyTreeReportsTests
    {
        private readonly ReportsTestsFixture _reportsTestsFixture;
        private readonly FamilyTreeReports _familyTreeReportsBusiness;
        private readonly ITestOutputHelper _testOutputHelper;

        public FamilyTreeReportsTests(ReportsTestsFixture reportsTestsFixture, ITestOutputHelper testOutputHelper)
        {
            _reportsTestsFixture = reportsTestsFixture;
            _familyTreeReportsBusiness = _reportsTestsFixture.GetFamilyTreeReports();
            _testOutputHelper = testOutputHelper;
        }

        [Theory(DisplayName = "Get Family Tree for Ancestor level")]
        [Trait("Category", "Family Tree Reports Tests")]
        [InlineData("ancestors", "John Armless")]
        public async Task GetAncestorsFamilyTreeBasedOnLevelByPersonTest(string level, string personFullName)
        {
            // Act
            await _familyTreeReportsBusiness.GetFamilyTreeBasedOnLevelByPerson(level, personFullName);

            // Assert
            _reportsTestsFixture.Mocker.GetMock<IAncestorsReportsRepository>()
                .Verify(a => a.GetAncestorsByIdAsync(It.IsAny<string>()), Times.Once);

            _testOutputHelper.WriteLine($"Executed test: GetAncestorsFamilyTreeBasedOnLevelByPersonTest");
        }

        [Theory(DisplayName = "Get Family Tree for Parents level")]
        [Trait("Category", "Family Tree Reports Tests")]
        [InlineData("parents", "John Armless")]
        public async Task GetParentsFamilyTreeBasedOnLevelByPersonTest(string level, string personFullName)
        {
            // Act
            await _familyTreeReportsBusiness.GetFamilyTreeBasedOnLevelByPerson(level, personFullName);

            // Assert
            _reportsTestsFixture.Mocker.GetMock<IParentsReportsRepository>()
                .Verify(p => p.GetParentsByIdAsync(It.IsAny<string>()), Times.Once);

            _testOutputHelper.WriteLine($"Executed test: GetParentsFamilyTreeBasedOnLevelByPersonTest");
        }

        [Theory(DisplayName = "Get Family Tree for Children level")]
        [Trait("Category", "Family Tree Reports Tests")]
        [InlineData("children", "John Armless")]
        public async Task GetChildrenFamilyTreeBasedOnLevelByPersonTest(string level, string personFullName)
        {
            // Act
            await _familyTreeReportsBusiness.GetFamilyTreeBasedOnLevelByPerson(level, personFullName);

            // Assert
            _reportsTestsFixture.Mocker.GetMock<IChildrenReportsRepository>()
                .Verify(p => p.GetChildrenByIdAsync(It.IsAny<string>()), Times.Once);

            _testOutputHelper.WriteLine($"Executed test: GetChildrenFamilyTreeBasedOnLevelByPersonTest");
        }
    }
}
