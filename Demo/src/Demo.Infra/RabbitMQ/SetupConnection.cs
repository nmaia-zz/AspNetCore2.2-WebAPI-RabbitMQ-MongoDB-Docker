using Demo.Contracts.RabbitMQ;
using RabbitMQ.Client;
using System;

namespace Demo.Infra.RabbitMQ
{
    public class SetupConnection
        : ISetupConnection
    {
        public IConnection OpenConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(@"amqp://user:pass@rabbitmq:5672/challenge-dev"),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                AutomaticRecoveryEnabled = true
            };

            return connectionFactory.CreateConnection();
        }
    }
}
