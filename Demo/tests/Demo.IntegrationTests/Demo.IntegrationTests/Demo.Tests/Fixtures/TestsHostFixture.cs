using AutoMapper;
using Demo.API.Configuration;
using Demo.Business.Contracts;
using Demo.Business.Notifications;
using Demo.Business.Reports;
using Demo.Business.Services;
using Demo.Infra.Contracts.MongoDB;
using Demo.Infra.Contracts.RabbitMQ;
using Demo.Infra.Contracts.Repository;
using Demo.Infra.Data;
using Demo.Infra.Mappings;
using Demo.Infra.RabbitMQ;
using Demo.Infra.RabbitMQ.HostedServices;
using Demo.Infra.Repository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Xunit;

namespace Demo.Tests
{
    /// <summary>
    /// One instance of this will be created per test collection.
    /// </summary>
    public class TestHostFixture : ICollectionFixture<CustomWebApplicationFactory>
	{
		public readonly HttpClient Client;

		public TestHostFixture()
		{
			var factory = new CustomWebApplicationFactory();
			Client = factory
				.WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(@"../../src/Demo.API"))
				.CreateClient();
		}
	}

	[CollectionDefinition("Integration tests collection")]
	public class IntegrationTestsCollection : ICollectionFixture<TestHostFixture>
	{
	}

	public class TestStartup// : Demo.API.Startup
	{
        public IConfiguration Configuration { get; }

        public TestStartup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                //.AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Enable CORS
            services.AddCors(options => {

                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin());

            });

            services.AddOptions();
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));
            services.Configure<RabbitMQSettings>(Configuration.GetSection("RabbitMQSettings"));

            MongoDBPersistence.Setup();

            services.AddAutoMapper(typeof(TestStartup));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(options => {

                options.SuppressModelStateInvalidFilter = true;

            });

            // Hosted Services
            //services.AddHostedService<ResearchConsumerHostedService>();
            //services.AddHostedService<AncestorsConsumerHostedService>();
            //services.AddHostedService<ChildrenConsumerHostedService>();
            //services.AddHostedService<ParentsConsumerHostedService>();

            // Services
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IResearchServices, ResearchServices>();

            // Repository
            // services.AddSingleton<IMongoDBContext, MongoDBContext>();
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

            services.AddSwaggerGen(s => {

                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "API Challenge", Version = "v1" });

            });
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

            app.UseSwagger();
            app.UseSwaggerUI(s => {

                s.SwaggerEndpoint("/swagger/v1/swagger.json", "API Challenge V1");

            });
        }
    }

    public class CustomWebApplicationFactory : WebApplicationFactory<TestStartup>
	{
		protected override IWebHostBuilder CreateWebHostBuilder()
		{
			return WebHost
				.CreateDefaultBuilder(Array.Empty<string>())
				.UseStartup<TestStartup>();
		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			base.ConfigureWebHost(builder);
			var environmentName = "Testing";
			if (string.IsNullOrEmpty(environmentName))
				throw new ArgumentException(
					$"{nameof(CustomWebApplicationFactory)}.{nameof(ConfigureWebHost)} needs environment variable ASPNETCORE_ENVIRONMENT to set environment.");
			builder.UseEnvironment(environmentName);
		}
	}
}
