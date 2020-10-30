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
            services.AddSingleton<IAncestorsReportsRepository, AncestorsReportsRepository>();
            services.AddSingleton<IChildrenReportsRepository, ChildrenReportsRepository>();
            services.AddSingleton<IParentsReportsRepository, ParentsReportsRepository>();

            // RabbitMQ
            services.AddScoped<ISetupConnection, SetupConnection>();
            services.AddScoped<IQueueManagementResearch, QueueManagementResearch>();
            services.AddScoped<IQueueManagementAncestorsReport, QueueManagementAncestors>();
            services.AddScoped<IQueueManagementChildrenReport, QueueManagementChildren>();
            services.AddScoped<IQueueManagementParentsReport, QueueManagementParents>();
            services.AddScoped<IAncestorsReportsPublisher, AncestorsReportsPublisher>();
            services.AddScoped<IChildrenReportsPublisher, ChildrenReportsPublisher>();
            services.AddScoped<IParentsReportsPublisher, ParentsReportsPublisher>();

            // Reports
            services.AddScoped<IRegionalReports, RegionalReports>();
            services.AddScoped<IFamilyTreeReports, FamilyTreeReports>();

            return services;
        }
    }
}
