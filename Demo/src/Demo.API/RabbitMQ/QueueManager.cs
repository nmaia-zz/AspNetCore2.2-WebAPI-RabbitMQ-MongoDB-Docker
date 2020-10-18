using Demo.API.Context;
using Demo.API.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Demo.API.RabbitMQ
{
    public class QueueManager
    {
        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            Uri = new Uri(@"amqp://user:pass@rabbitmq:5672/challenge-dev"),
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            AutomaticRecoveryEnabled = true
        };

        public void Publish(Pesquisa pesquisa)
        {
            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: "myqueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );

                        string message = JsonConvert.SerializeObject(pesquisa);
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: "",
                            routingKey: "myqueue",
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

        public void Subcribe(string fila)
        {
            try
            {
                using (var connection = connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: fila,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);

                            var pesquisa = JsonConvert.DeserializeObject<Pesquisa>(message);
                            new MongoDBContext().InsereItem<Pesquisa>("Pesquisas", pesquisa);
                        };

                        channel.BasicConsume(
                            queue: fila,
                            autoAck: true,
                            consumer: consumer
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
