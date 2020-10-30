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
    public class ChildrenReportsPublisherTests
    {
        private readonly ResearchTestsFixture _researchTestsFixture;

        public ChildrenReportsPublisherTests(ResearchTestsFixture researchTestsFixture)
        {
            _researchTestsFixture = researchTestsFixture;
        }

        [Fact(DisplayName = "Mount Children Report Object")]
        [Trait("Category", "Family Tree Reports Tests")]
        public void ChildrenReporMounttObjectTest()
        {
            //Arrange
            var research = _researchTestsFixture.CreateValidResearch();
            var iQueueManagementChildrenReport = new Mock<IQueueManagementChildrenReport>();
            var childrenReports = new ChildrenReportsPublisher(iQueueManagementChildrenReport.Object);

            // Act
            var childReportPublisher = childrenReports.MountChildrenObjectToInsert(research);            

            // Assert
            childReportPublisher.Should().BeOfType(typeof(ChildrenReport), "The method should return the type: ChildrenReport");
        }
    }
}
