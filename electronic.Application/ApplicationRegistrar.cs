using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace electronic.Application
{
    public static class ApplicationRegistrar
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection builder)
        {
            return builder;
        }

        public static IApplicationBuilder AddApplicationApp(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
