using Demo.Domain.Entities;

namespace Demo.Contracts.RabbitMQ
{
    public interface IQueueManagementChildrenReport
        : IQueuePublisher<ChildrenReport>
    {
    }
}
