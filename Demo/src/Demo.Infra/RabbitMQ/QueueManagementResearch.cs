using Demo.Contracts.RabbitMQ;
using Demo.Domain.Entities;
using Demo.Infra.RabbitMQ.Base;

namespace Demo.Infra.RabbitMQ
{
    public class QueueManagementResearch
        : BaseQueueManagement<Research>
        , IQueueManagementResearch
    {
        public QueueManagementResearch(ISetupConnection connectionFactory)
            : base(connectionFactory)
        {

        }
    }
}
