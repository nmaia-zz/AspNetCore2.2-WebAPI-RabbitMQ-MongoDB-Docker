using Demo.Domain.Entities;

namespace Demo.Infra.Contracts.RabbitMQ
{
    public interface IQueueManagementParentsReport
        : IQueuePublisher<ParentsReport>
    {
    }
}
