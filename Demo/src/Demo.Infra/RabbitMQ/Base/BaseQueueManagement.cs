using Demo.Contracts.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

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

        public void Publish(TEntity obj, string queue, string exchange, string routingKey)
        {
            try
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
