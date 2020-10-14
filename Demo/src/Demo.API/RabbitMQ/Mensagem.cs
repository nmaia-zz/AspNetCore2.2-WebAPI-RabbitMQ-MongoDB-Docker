using RabbitMQ.Client;
using System;
using System.Text;

namespace Demo.API.RabbitMQ
{
    public class Mensagem
    {
        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            Uri = new Uri(@"amqp://user:pass@rabbitmq:5672/challenge-dev"),
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            AutomaticRecoveryEnabled = true
        };

        public void Publicar()
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

                    string message = "Minha Mensagem - " + DateTime.Now.ToString();
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

        public void Consomir()
        {

        }
    }
}
