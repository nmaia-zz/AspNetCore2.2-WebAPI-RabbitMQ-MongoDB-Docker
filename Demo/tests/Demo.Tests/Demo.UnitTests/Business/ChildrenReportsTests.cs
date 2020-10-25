using Demo.Business.Reports;
using Demo.Contracts.Business;
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

namespace Demo.UnitTests.Business
{
    public class ChildrenReportsTests
    {
        [Fact]
        public void ChildrenReporMounttObjectTest()
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

            var iChildrenReportsRepository = new Moq.Mock<IChildrenReportsRepository>();
            var iChildrenReports = new Moq.Mock<IChildrenReportsPublisher>();
            var iQueueManagementChildrenReport = new Moq.Mock<IQueueManagementChildrenReport>();

            var childrenReports = new ChildrenReportsPublisher(iQueueManagementChildrenReport.Object);

            // Act
            var childReportPublisher = childrenReports.MountChildrenObjectToInsert(research);            
            iChildrenReportsRepository.Setup(x => x.GetChildrenByIdAsync(It.IsAny<string>())).ReturnsAsync(childReportPublisher);           

            // Assert
            childReportPublisher.Should().BeOfType(typeof(ChildrenReport), "The method should return a type of 'ChildrenReport'");
        }
    }
}
