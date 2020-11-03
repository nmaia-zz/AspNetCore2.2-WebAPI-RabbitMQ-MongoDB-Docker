using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.API.Configuration
{
    public static class CorsConfig
    {
        public static readonly string _AllowAnyOriginPolicy = "AllowAnyOriginPolicy";

        public static IServiceCollection AddMyCorsConfig(this IServiceCollection services) 
        {
            services.AddCors(options => {

                options.AddPolicy(name: _AllowAnyOriginPolicy,
                    builder => {

                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();

                    });
            });

            return services;
        }

        public static IApplicationBuilder UseMyCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors(_AllowAnyOriginPolicy);

            return app;
        }
    }
}
