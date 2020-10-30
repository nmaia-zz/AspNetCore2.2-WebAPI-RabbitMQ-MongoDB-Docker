using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.RabbitMQ.Base;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagementChildren
        : BaseQueueManagement<ChildrenReport>
        , IQueueManagementChildrenReport
    {
        public QueueManagementChildren(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}
