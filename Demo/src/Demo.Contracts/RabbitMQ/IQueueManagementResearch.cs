using Demo.Domain.Entities;
using System.Threading.Tasks;

namespace Demo.Contracts.RabbitMQ
{
    public interface IQueueManagementResearch 
        : IQueuePublisher<Research>
    {

    }
}
