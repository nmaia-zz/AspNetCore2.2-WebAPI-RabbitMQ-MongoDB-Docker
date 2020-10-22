﻿using Demo.Contracts.RabbitMQ;
using Demo.Domain.Entities;
using Demo.Infra.RabbitMQ.Base;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagementParents
        : BaseQueueManagement<ParentsReport>
        , IQueueManagementParentsReport
    {
        public QueueManagementParents(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}