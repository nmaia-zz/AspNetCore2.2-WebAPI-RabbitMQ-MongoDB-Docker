using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.RabbitMQ.Base;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagerResearch
        : BaseQueueManagement<Research>
        , IQueueManagerResearch
    {
        public QueueManagerResearch(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}
