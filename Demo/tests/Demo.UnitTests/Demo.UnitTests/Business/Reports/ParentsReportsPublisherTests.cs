using Demo.Business.Reports;
using Demo.Contracts.RabbitMQ;
using Demo.Domain.Entities;
using Demo.UnitTests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace Demo.UnitTests.Business.Reports
{
    [Collection(nameof(ResearchCollection))]
    public class ParentsReportsPublisherTests
    {
        private readonly ResearchTestsFixture _researchTestsFixture;

        public ParentsReportsPublisherTests(ResearchTestsFixture researchTestsFixture)
        {
            _researchTestsFixture = researchTestsFixture;
        }

        [Fact(DisplayName = "Mount Parents Report Object")]
        [Trait("Category", "Family Tree Reports Tests")]
        public void ParentsReporMounttObjectTest()
        {
            //Arrange
            var research = _researchTestsFixture.CreateValidResearch();
            var iQueueManagementParentsReport = new Mock<IQueueManagementParentsReport>();
            var parentsReports = new ParentsReportsPublisher(iQueueManagementParentsReport.Object);

            // Act
            var parentsReportPublisher = parentsReports.MountParentsObjectToInsert(research);                      

            // Assert
            parentsReportPublisher.Should().BeOfType(typeof(ParentsReport), "The method should return the type: ParentsReport");
        }
    }
}
