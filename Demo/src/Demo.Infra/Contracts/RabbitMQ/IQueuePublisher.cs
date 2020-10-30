using System;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.RabbitMQ
{
    public interface IQueuePublisher<TEntity> : IDisposable 
        where TEntity : class
    {
        Task Publish(TEntity obj, string queue, string exchange, string routingKey);
    }
}
