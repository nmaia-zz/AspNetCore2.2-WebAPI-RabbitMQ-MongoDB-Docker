﻿namespace Demo.Contracts.RabbitMQ
{
    public interface IQueuePublisher<TEntity> 
        where TEntity : class
    {
        void Publish(TEntity obj, string queue, string exchange, string routingKey);
    }
}