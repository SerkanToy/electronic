using electronic.Application.Abstracts;
using electronic.Application.Interface;
using electronic.Infrastructure.Context;
using electronic.Infrastructure.Options;
using electronic.Infrastructure.Processors;
using electronic.Infrastructure.Repositories;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.AddIdentityCore<UserApp>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 4;

                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                
            }).AddRoles<RoleApp>()
            .AddEntityFrameworkStores<CilingirogluDbContext>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => { 
                var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>() ?? throw new ArgumentException(nameof(JwtOptions));
                jwt.TokenValidationParameters = new TokenValidationParameters { 
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.Secret))
                };
                jwt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Cookies["ACCESS_TOKEN"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IAuthTokenProcessor), typeof(AuthTokenProcessor));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
