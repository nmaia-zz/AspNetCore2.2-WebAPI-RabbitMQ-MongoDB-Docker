using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.RabbitMQ.Base;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagerChildrenTree
        : BaseQueueManagement<ChildrenTree>
        , IQueueManagerChildrenTree
    {
        public QueueManagerChildrenTree(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}
