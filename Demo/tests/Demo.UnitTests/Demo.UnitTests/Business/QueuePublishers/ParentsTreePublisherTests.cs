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
    public class ParentsTreePublisherTests
    {
        private readonly ResearchTestsFixture _researchTestsFixture;

        public ParentsTreePublisherTests(ResearchTestsFixture researchTestsFixture)
        {
            _researchTestsFixture = researchTestsFixture;
        }

        [Fact(DisplayName = "Mount Parents Tree Object")]
        [Trait("Category", "Family Tree Reports")]
        public void ParentsReporMounttObjectTest()
        {
            //Arrange
            var research = _researchTestsFixture.CreateOneValidResearch();
            var iQueueManagementParentsReport = new Mock<IQueueManagerParentsTree>();
            var parentsReports = new ParentsTreePublisher(iQueueManagementParentsReport.Object);

            // Act
            var parentsReportPublisher = parentsReports.MountParentsObjectToInsert(research);                      

            // Assert
            parentsReportPublisher.Should().BeOfType(typeof(ParentsTree), "The method should return the type: ParentsReport");
        }
    }
}
