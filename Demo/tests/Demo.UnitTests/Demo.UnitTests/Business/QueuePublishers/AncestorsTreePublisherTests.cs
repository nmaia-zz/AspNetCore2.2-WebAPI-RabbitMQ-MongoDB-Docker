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
    public class AncestorsTreePublisherTests
    {
        private readonly ResearchTestsFixture _researchTestsFixture;

        public AncestorsTreePublisherTests(ResearchTestsFixture researchTestsFixture)
        {
            _researchTestsFixture = researchTestsFixture;
        }

        [Fact(DisplayName ="Mount Ancestors Tree Object")]
        [Trait("Category", "Family Tree Reports")]
        public void AncestorsReporMounttObjectTest()
        {
            //Arrange
            var research = _researchTestsFixture.CreateOneValidResearch();
            var iQueueManagementAncestorsReport = new Mock<IQueueManagerAncestorsTree>();
            var ancestorsReports = new AncestorsTreePublisher(iQueueManagementAncestorsReport.Object);

            // Act
            var ancestorsReportsPublisher = ancestorsReports.MountAncestorObjectToInsert(research);            

            // Assert
            ancestorsReportsPublisher.Should().BeOfType(typeof(AncestorsTree), "The method should return the type: AncestorsReport");
        }
    }
}
