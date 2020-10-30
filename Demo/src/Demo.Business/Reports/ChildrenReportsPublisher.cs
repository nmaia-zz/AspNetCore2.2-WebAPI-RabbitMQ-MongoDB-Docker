﻿using Demo.Business.Contracts;
using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Business.Reports
{
    public class ChildrenReportsPublisher : IChildrenReportsPublisher
    {
        private readonly IQueueManagementChildrenReport _queueManagementChildren;

        public ChildrenReportsPublisher(IQueueManagementChildrenReport queueManagementChildren)
        {
            _queueManagementChildren = queueManagementChildren;
        }

        public ChildrenReport MountChildrenObjectToInsert(Research research)
        {
            if (research.Person.Children.Any())
            {
                var children = new string[research.Person.Children.Count()];
                var index = 0;

                foreach (var child in research.Person.Children)
                {
                    children[index] = child;
                    index++;
                }

                var childrenObject = new ChildrenReport()
                {
                    Id = string.Join(" ", research.Person.FirstName, research.Person.LastName),
                    Children = children
                };

                return childrenObject;
            }

            return null;
        }

        public async Task PublishToBeAddedIntoFamilyTree(Research research)
        {
            var children = MountChildrenObjectToInsert(research);

            if (children != null)
                await _queueManagementChildren.Publish(children, "children.queue", "children.exchange", "children.queue*");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
