using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Demo.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s => {

                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "API Asp.Net Core 2.2 Sample",
                    Description = "This project was developed using the following technologies: RabbitMQ, MonogDB, xUnit, FluentAssertions and Asp.Net Core 2.2 Web API.",
                    Contact = new Contact
                    {
                        Name = "Natanael Maia",
                        Url = "https://github.com/nmaia"
                    },
                    License = new License 
                    {
                        Name = "MIT License",
                        Url = "https://mit-license.org/"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

            });

            return services;
        }

        public static IApplicationBuilder UseMySwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => {

                s.SwaggerEndpoint("/swagger/v1/swagger.json", "API Asp.Net Core 2.2 Sample V1");

            });

            return app;
        }
    }
}
