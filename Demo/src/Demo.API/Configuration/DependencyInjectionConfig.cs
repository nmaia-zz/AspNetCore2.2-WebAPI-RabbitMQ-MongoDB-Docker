using Microsoft.Extensions.DependencyInjection;
using Demo.Business.Reports;
using Demo.Infra.RabbitMQ.HostedServices;
using Demo.Infra.Repository;
using Demo.Infra.Data;
using Demo.Infra.RabbitMQ;
using Demo.Infra.Contracts.MongoDB;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Business.Contracts;
using Demo.Infra.Contracts.Repository;
using Demo.Business.Services;
using Demo.Business.Notifications;
using Demo.Business.Contracts.Services;
using Demo.Business.Contracts.QueuePublishers;
using Demo.Business.QueuePublishers;

namespace Demo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        // Extension Method
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Hosted Services
            services.AddHostedService<ResearchConsumerHostedService>();
            services.AddHostedService<AncestorsConsumerHostedService>();
            services.AddHostedService<ChildrenConsumerHostedService>();
            services.AddHostedService<ParentsConsumerHostedService>();

            // Services
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IResearchServices, ResearchServices>();

            // Repository
            services.AddSingleton<IMongoDBContext, MongoDBContext>();
            services.AddSingleton<IResearchRepository, ResearchRepository>();
            services.AddSingleton<IAncestorsTreeRepository, AncestorsTreeRepository>();
            services.AddSingleton<IChildrenTreeRepository, ChildrenTreeRepository>();
            services.AddSingleton<IParentsTreeRepository, ParentsTreeRepository>();

            // RabbitMQ
            services.AddScoped<ISetupConnection, SetupConnection>();
            services.AddScoped<IQueueManagerResearch, QueueManagerResearch>();
            services.AddScoped<IQueueManagerAncestorsTree, QueueManagerAncestorsTree>();
            services.AddScoped<IQueueManagerChildrenTree, QueueManagerChildrenTree>();
            services.AddScoped<IQueueManagerParentsTree, QueueManagerParentsTree>();
            services.AddScoped<IAncestorsTreePublisher, AncestorsTreePublisher>();
            services.AddScoped<IChildrenTreePublisher, ChildrenTreePublisher>();
            services.AddScoped<IParentsTreePublisher, ParentsTreePublisher>();

            // Reports
            services.AddScoped<IRegionalReports, RegionalReports>();
            services.AddScoped<IFamilyTreeReports, FamilyTreeReports>();

            return services;
        }
    }
}
