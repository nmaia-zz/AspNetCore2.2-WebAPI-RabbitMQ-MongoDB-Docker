using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.RabbitMQ.Base;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagementAncestors
        : BaseQueueManagement<AncestorsReport>
        , IQueueManagementAncestorsReport
    {
        public QueueManagementAncestors(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}
