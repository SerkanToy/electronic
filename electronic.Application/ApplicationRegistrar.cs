using Microsoft.Extensions.DependencyInjection;

namespace electronic.Application
{
    public static class ApplicationRegistrar
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            return service;
        }
    }
}
