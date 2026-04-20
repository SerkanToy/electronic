using electronic.Application.Services;
using electronic.Domain.Entities.Employees;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace electronic.Infrastructure.Services
{
    public static class ProductService
    {
        public static string CreateProductAsync()
        {
            
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();

            UserManager<UserApp> userManager = httpContextAccessor
                .HttpContext
                .RequestServices
                .GetRequiredService<UserManager<UserApp>>();

            UserApp appUser = userManager.Users.First(p => p.Id == Guid.Parse("019D8C03-C817-734F-9298-D828BD40372F"));

            return appUser.Name + " " + appUser.SurName + " (" + appUser.Email + ")";
        }
        /*
        public async Task<string> GetUpdateUserName(Product product)
        {
            HttpContextAccessor httpContextAccessor = new();
            var userManager = httpContextAccessor
                .HttpContext
                .RequestServices
                .GetRequiredService<UserManager<UserApp>>();

            UserApp appUser = userManager.Users.First(p => p.Id == product.CreateUserId);

            return appUser.Name + " " + appUser.SurName + " (" + appUser.Email + ")";
        }

        public async Task<string> GetDeleteUserName(Product product)
        {
            HttpContextAccessor httpContextAccessor = new();
            var userManager = httpContextAccessor
                .HttpContext
                .RequestServices
                .GetRequiredService<UserManager<UserApp>>();

            UserApp appUser = userManager.Users.First(p => p.Id == product.CreateUserId);

            return appUser.Name + " " + appUser.SurName + " (" + appUser.Email + ")";
        }*/
    }
}
