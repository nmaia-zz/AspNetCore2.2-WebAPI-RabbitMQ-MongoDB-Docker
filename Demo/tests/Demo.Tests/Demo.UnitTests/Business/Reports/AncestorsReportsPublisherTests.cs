using Demo.Business.Reports;
using Demo.Contracts.RabbitMQ;
using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Demo.Domain.Enums;
using FluentAssertions;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Demo.UnitTests.Business.Reports
{
    public class AncestorsReportsPublisherTests
    {
        [Fact]
        public void AncestorsReporMounttObjectTest()
        {
            //Arrange
            var research = new Research() { 
            
                Id = ObjectId.GenerateNewId().ToString(),
                Region = (Region)Enum.Parse(typeof(Region), "SOUTHEAST_REGION"),
                Person = new Person() { 
                
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = "Leia",
                    LastName = "Skywalker",
                    Children = new List<Person>(),
                    Filiation = new Person[2],
                    Gender = (Gender)Enum.Parse(typeof(Gender), "FEMALE"),
                    Schooling = (Schooling)Enum.Parse(typeof(Schooling), "MASTERS"),
                    SkinColor = (SkinColor)Enum.Parse(typeof(SkinColor), "WHITE")
                }

            };

            var iAncestorsReportsRepository = new Moq.Mock<IAncestorsReportsRepository>();
            var iQueueManagementAncestorsReport = new Moq.Mock<IQueueManagementAncestorsReport>();

            var ancestorsReports = new AncestorsReportsPublisher(iQueueManagementAncestorsReport.Object);

            // Act
            var ancestorsReportPublisher = ancestorsReports.MountAncestorObjectToInsert(research);            
            iAncestorsReportsRepository.Setup(x => x.GetAncestorsByIdAsync(It.IsAny<string>())).ReturnsAsync(ancestorsReportPublisher);           

            // Assert
            ancestorsReportPublisher.Should().BeOfType(typeof(AncestorsReport), "The method should return the type: AncestorsReport");
        }
    }
}
