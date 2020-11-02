using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.Contracts.RabbitMQ.Base;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infra.RabbitMQ.Base
{
    public abstract class BaseQueueManagement<TEntity>
        : IQueuePublisher<TEntity>
        where TEntity : class
    {
        protected readonly ISetupConnection _connectionFactory;

        protected BaseQueueManagement(ISetupConnection connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public virtual async Task Publish(TEntity obj, string queue, string exchange, string routingKey)
        {
            using (var connection = _connectionFactory.OpenConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                        exchange: exchange,
                        type: ExchangeType.Topic
                    );

                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    string message = JsonConvert.SerializeObject(obj);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: exchange,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: body
                    );
                }
            }

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
