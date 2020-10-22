using Demo.Contracts.RabbitMQ;
using Demo.Domain.Entities;
using Demo.Infra.RabbitMQ.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagementAncestors
        : BaseQueueManagement<AncestorsReport>
        , IQueueManagementAncestorReport
    {
        public QueueManagementAncestors(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}
