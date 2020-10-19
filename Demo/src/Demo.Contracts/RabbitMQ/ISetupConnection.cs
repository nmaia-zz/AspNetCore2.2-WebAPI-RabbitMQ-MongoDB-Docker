using RabbitMQ.Client;

namespace Demo.Contracts.RabbitMQ
{
    public interface ISetupConnection
    {
        IConnection OpenConnection();
    }
}
