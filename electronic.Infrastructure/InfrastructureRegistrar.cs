using electronic.Application.Interfaces;
using electronic.Application.UoW;
using electronic.Infrastructure.Context;
using electronic.Infrastructure.Models;
using electronic.Infrastructure.Repositories;
using electronic.Infrastructure.UoW;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
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

            services.AddIdentityCore<UserApp>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireUppercase = true;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            }).AddRoles<RoleApp>()
            .AddSignInManager<SignInManager<UserApp>>()
            .AddEntityFrameworkStores<CilingirogluDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(ResponseModel));
            services.AddTransient(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
