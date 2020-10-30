using RabbitMQ.Client;
using System;

namespace Demo.Infra.Contracts.RabbitMQ
{
    public interface ISetupConnection : IDisposable
    {
        IConnection OpenConnection();
    }
}
