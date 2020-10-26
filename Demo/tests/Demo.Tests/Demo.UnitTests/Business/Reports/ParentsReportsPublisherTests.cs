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
    public class ParentsReportsPublisherTests
    {
        [Fact]
        public void ParentsReporMounttObjectTest()
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

            var iParentsReportsRepository = new Moq.Mock<IParentsReportsRepository>();
            var iQueueManagementParentsReport = new Moq.Mock<IQueueManagementParentsReport>();

            var parentsReports = new ParentsReportsPublisher(iQueueManagementParentsReport.Object);

            // Act
            var parentsReportPublisher = parentsReports.MountParentsObjectToInsert(research);            
            iParentsReportsRepository.Setup(x => x.GetParentsByIdAsync(It.IsAny<string>())).ReturnsAsync(parentsReportPublisher);           

            // Assert
            parentsReportPublisher.Should().BeOfType(typeof(ParentsReport), "The method should return the type: ParentsReport");
        }
    }
}
