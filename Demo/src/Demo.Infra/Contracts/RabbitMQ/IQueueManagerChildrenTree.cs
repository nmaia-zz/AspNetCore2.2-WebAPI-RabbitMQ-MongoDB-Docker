using Demo.Domain.Entities;
using Demo.Infra.Contracts.RabbitMQ.Base;

namespace Demo.Infra.Contracts.RabbitMQ
{
    public interface IQueueManagerChildrenTree
        : IQueuePublisher<ChildrenTree>
    {
    }
}
