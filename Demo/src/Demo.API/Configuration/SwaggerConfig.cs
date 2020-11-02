using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s => {

                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "API Asp.Net Core 2.2 Sample", Version = "v1" });

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
