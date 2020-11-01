using System;
using System.Threading.Tasks;

namespace Demo.Infra.Contracts.RabbitMQ.Base
{
    public interface IQueuePublisher<TEntity> : IDisposable 
        where TEntity : class
    {
        Task Publish(TEntity obj, string queue, string exchange, string routingKey);
    }
}
