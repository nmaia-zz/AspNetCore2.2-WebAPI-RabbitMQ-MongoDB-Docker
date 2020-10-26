using AutoMapper;
using Demo.Business.Reports;
using Demo.Contracts.Business;
using Demo.Contracts.Database;
using Demo.Contracts.RabbitMQ;
using Demo.Contracts.Repository;
using Demo.Infra.Data;
using Demo.Infra.Mappings;
using Demo.Infra.RabbitMQ;
using Demo.Infra.RabbitMQ.HostedServices;
using Demo.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Demo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable CORS
            services.AddCors(options => {

                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin());

            });

            MongoDBPersistence.Setup();

            services.AddHostedService<ResearchConsumerHostedService>();
            services.AddHostedService<AncestorsConsumerHostedService>();
            services.AddHostedService<ChildrenConsumerHostedService>();
            services.AddHostedService<ParentsConsumerHostedService>();
            
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<IMongoDBContext, MongoDBContext>();                        
            services.AddSingleton<IResearchRepository, ResearchRespository>();
            services.AddSingleton<IAncestorsReportsRepository, AncestorsReportsRepository>();
            services.AddSingleton<IChildrenReportsRepository, ChildrenReportsRepository>();
            services.AddSingleton<IParentsReportsRepository, ParentsReportsRepository>();

            services.AddScoped<ISetupConnection, SetupConnection>();
            services.AddScoped<IQueueManagementResearch, QueueManagementResearch>();
            services.AddScoped<IQueueManagementAncestorsReport, QueueManagementAncestors>();
            services.AddScoped<IQueueManagementChildrenReport, QueueManagementChildren>();
            services.AddScoped<IQueueManagementParentsReport, QueueManagementParents>();
            
            services.AddScoped<IRegionalReports, RegionalReports>();
            services.AddScoped<IAncestorsReportsPublisher, AncestorsReportsPublisher>();
            services.AddScoped<IChildrenReportsPublisher, ChildrenReportsPublisher>();
            services.AddScoped<IParentsReportsPublisher, ParentsReportsPublisher>();
            services.AddScoped<IFamilyTreeReports, FamilyTreeReports>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
