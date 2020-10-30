using Demo.Infra.Contracts.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;

namespace Demo.Infra.RabbitMQ
{
    public class SetupConnection
        : ISetupConnection
    {
        private readonly IOptions<RabbitMQSettings> _rabbitMQSettings;

        public SetupConnection(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings;
        }

        public IConnection OpenConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(_rabbitMQSettings.Value.Uri),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                AutomaticRecoveryEnabled = true
            };

            return connectionFactory.CreateConnection();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
