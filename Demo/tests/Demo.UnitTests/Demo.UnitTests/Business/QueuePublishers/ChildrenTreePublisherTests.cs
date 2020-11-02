using Demo.Business.QueuePublishers;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.UnitTests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace Demo.UnitTests.Business.QueuePublishers
{
    [Collection(nameof(ResearchCollection))]
    public class ChildrenTreePublisherTests
    {
        private readonly ResearchTestsFixture _researchTestsFixture;

        public ChildrenTreePublisherTests(ResearchTestsFixture researchTestsFixture)
        {
            _researchTestsFixture = researchTestsFixture;
        }

        [Fact(DisplayName = "Mount Children Tree Object")]
        [Trait("Category", "Family Tree Reports")]
        public void ChildrenReporMounttObjectTest()
        {
            //Arrange
            var research = _researchTestsFixture.CreateOneValidResearch();
            var iQueueManagementChildrenReport = new Mock<IQueueManagerChildrenTree>();
            var childrenReports = new ChildrenTreePublisher(iQueueManagementChildrenReport.Object);

            // Act
            var childReportPublisher = childrenReports.MountChildrenObjectToInsert(research);            

            // Assert
            childReportPublisher.Should().BeOfType(typeof(ChildrenTree), "The method should return the type: ChildrenReport");
        }
    }
}
