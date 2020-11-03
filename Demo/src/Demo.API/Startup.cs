using AutoMapper;
using Demo.API.Configuration;
using Demo.Infra.Data;
using Demo.Infra.Mappings;
using Demo.Infra.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));
            services.Configure<RabbitMQSettings>(Configuration.GetSection("RabbitMQSettings"));

            MongoDBPersistence.Setup();

            services.AddAutoMapper(typeof(Startup));

            services.AddMyCorsConfig();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(options => {

                options.SuppressModelStateInvalidFilter = true;

            });

            services.ResolveDependencies();

            services.AddSwagger();
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

            app.UseMyCorsConfig();

            app.UseMvc();

            app.UseMySwaggerConfig();
        }
    }
}
