using Demo.Business.Reports;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.UnitTests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace Demo.UnitTests.Business.Reports
{
    [Collection(nameof(ResearchCollection))]
    public class AncestorsReportsPublisherTests
    {
        private readonly ResearchTestsFixture _researchTestsFixture;

        public AncestorsReportsPublisherTests(ResearchTestsFixture researchTestsFixture)
        {
            _researchTestsFixture = researchTestsFixture;
        }

        [Fact(DisplayName ="Mount Ancestors Report Object")]
        [Trait("Category", "Family Tree Reports Tests")]
        public void AncestorsReporMounttObjectTest()
        {
            //Arrange
            var research = _researchTestsFixture.CreateValidResearch();
            var iQueueManagementAncestorsReport = new Mock<IQueueManagementAncestorsReport>();
            var ancestorsReports = new AncestorsReportsPublisher(iQueueManagementAncestorsReport.Object);

            // Act
            var ancestorsReportsPublisher = ancestorsReports.MountAncestorObjectToInsert(research);            

            // Assert
            ancestorsReportsPublisher.Should().BeOfType(typeof(AncestorsReport), "The method should return the type: AncestorsReport");
        }
    }
}
