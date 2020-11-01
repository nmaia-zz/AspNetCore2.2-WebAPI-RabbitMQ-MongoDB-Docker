using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.RabbitMQ.Base;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagerParentsTree
        : BaseQueueManagement<ParentsTree>
        , IQueueManagerParentsTree
    {
        public QueueManagerParentsTree(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}
