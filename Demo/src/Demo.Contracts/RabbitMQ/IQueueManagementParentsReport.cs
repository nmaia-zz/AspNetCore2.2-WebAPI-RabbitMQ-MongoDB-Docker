using Demo.Domain.Entities;

namespace Demo.Contracts.RabbitMQ
{
    public interface IQueueManagementParentsReport
        : IQueuePublisher<ParentsReport>
    {
    }
}
