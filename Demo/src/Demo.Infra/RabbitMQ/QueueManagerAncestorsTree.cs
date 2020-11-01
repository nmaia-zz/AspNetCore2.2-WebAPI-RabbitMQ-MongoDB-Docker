using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.RabbitMQ.Base;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagerAncestorsTree
        : BaseQueueManagement<AncestorsTree>
        , IQueueManagerAncestorsTree
    {
        public QueueManagerAncestorsTree(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}
