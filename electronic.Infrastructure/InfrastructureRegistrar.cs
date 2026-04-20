using electronic.Application.Interface;
using electronic.Application.Services;
using electronic.Infrastructure.Context;
using electronic.Infrastructure.Repositories;
using electronic.Infrastructure.Services;
using electronik.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace electronic.Infrastructure
{
    public static class InfrastructureRegistrar
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CilingirogluDbContext>(opt =>
            {
                string connectionString = configuration.GetConnectionString("SqlServer")!;
                opt.UseSqlServer(connectionString);
            });


            services.AddIdentityCore<UserApp>()
                .AddRoles<RoleApp>()
                .AddEntityFrameworkStores<CilingirogluDbContext>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
