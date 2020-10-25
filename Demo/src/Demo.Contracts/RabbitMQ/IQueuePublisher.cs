using System.Threading.Tasks;

namespace Demo.Contracts.RabbitMQ
{
    public interface IQueuePublisher<TEntity> 
        where TEntity : class
    {
        Task Publish(TEntity obj, string queue, string exchange, string routingKey);
    }
}
