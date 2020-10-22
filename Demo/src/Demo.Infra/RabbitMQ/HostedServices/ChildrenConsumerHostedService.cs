using Demo.Contracts.Repository;
using Demo.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Infra.RabbitMQ.HostedServices
{
    // TODO: Verificar o ponto para implementar o BasicNack (retornar com a msg para a fila em caso de falha)
    public class ChildrenConsumerHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;
        private readonly IChildrenRepository _childrenRepository;

        public ChildrenConsumerHostedService(ILoggerFactory loggerFactory, IChildrenRepository childrenRepository)
        {
            this._logger = loggerFactory.CreateLogger<ChildrenConsumerHostedService>();
            InitRabbitMQ();
            _childrenRepository = childrenRepository;
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "user",
                Password = "pass",
                VirtualHost = "challenge-dev"
            };

            // create connection
            _connection = factory.CreateConnection();

            // create channel
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("children.exchange", ExchangeType.Topic);
            _channel.QueueDeclare("children.queue", false, false, false, null);
            _channel.QueueBind("children.queue", "children.exchange", "children.queue*", null);
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message
                var body = ea.Body.ToArray();
                var content = System.Text.Encoding.UTF8.GetString(body);

                // handle the received message
                HandleMessage(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("children.queue", false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            _logger.LogInformation($"consumer received {content}");

            var responseChildrenReport = JsonConvert.DeserializeObject<ChildrenReport>(content);
            _childrenRepository.Add(responseChildrenReport);
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation($"connection shut down {e.ReplyText}");
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation($"consumer cancelled {e.ConsumerTags}");
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation($"consumer unregistered {e.ConsumerTags}");
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation($"consumer registered {e.ConsumerTags}");
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation($"consumer shutdown {e.ReplyText}");
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
