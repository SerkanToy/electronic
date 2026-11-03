using electronic.Application.Interfaces;
using electronic.Application.IService;
using electronic.Application.UoW;
using electronic.Infrastructure.Context;
using electronic.Infrastructure.Models;
using electronic.Infrastructure.Repositories;
using electronic.Infrastructure.Service;
using electronic.Infrastructure.UoW;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtOptions:Issuer"],
                        ValidAudience = configuration["JwtOptions:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtOptions:Secret"]!)),
                        ClockSkew = TimeSpan.Zero
                    };

            }).AddCookie();

            services.AddCors(options => {
                options.AddPolicy("electronic.api.clientapp", builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();

                });
                options.AddPolicy("electronic.api.dashboard", builder =>
                {
                    builder.WithOrigins("http://localhost:4000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();

                });
            });


            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(ResponseModel));
            services.AddScoped(typeof(ResponseModel<>));
            services.AddTransient(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddTransient(typeof(IAuthService),typeof(AuthService));
            services.AddTransient(typeof(ITokenService),typeof(TokenService));
            services.AddTransient(typeof(IPhotographUpload),typeof(PhotographUpload));
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
