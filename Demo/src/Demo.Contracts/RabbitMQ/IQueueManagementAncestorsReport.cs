using Demo.Domain.Entities;

namespace Demo.Contracts.RabbitMQ
{
    public interface IQueueManagementAncestorsReport
        : IQueuePublisher<AncestorsReport>
    {
    }
}
