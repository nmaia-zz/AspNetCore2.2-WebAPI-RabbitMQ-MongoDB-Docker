using System;

namespace Demo.Contracts.RabbitMQ
{
    public interface IQueuePublisher<TEntity>
        : IDisposable 
        where TEntity : class
    {
        void Publish(TEntity obj, string queue, string exchange, string routingKey);
    }
}
