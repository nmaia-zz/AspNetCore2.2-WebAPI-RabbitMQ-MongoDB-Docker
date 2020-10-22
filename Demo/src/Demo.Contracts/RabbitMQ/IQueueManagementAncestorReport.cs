using Demo.Domain.Entities;

namespace Demo.Contracts.RabbitMQ
{
    public interface IQueueManagementAncestorReport
        : IQueuePublisher<AncestorsReport>
    {
    }
}
