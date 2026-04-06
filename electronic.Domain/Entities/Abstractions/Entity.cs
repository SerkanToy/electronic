using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Domain.Entities.Abstractions
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.CreateVersion7();
        }
        public Guid Id { get; set; }

        #region Audit Log
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreateAt { get; set; }
        public Guid CreateUserId { get; set; } = default!;
        public string CreateUserName => GetCreateUserName();
        public DateTimeOffset? UpdateAt { get; set; }
        public Guid? UpdateUserId { get; set; }
        public string? UpdateUserName => GetUpdateUserName();
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
        public Guid? DeleteUserId { get; set; }
        private string GetCreateUserName()
        {
            HttpContextAccessor httpContextAccessor = new();
            var userManager = httpContextAccessor
                .HttpContext
                .RequestServices
                .GetRequiredService<UserManager<UserApp>>();

            UserApp appUser = userManager.Users.First(p => p.Id == CreateUserId);

            return appUser.Name + " " + appUser.SurName + " (" + appUser.Email + ")";
        }

        private string? GetUpdateUserName()
        {
            if (UpdateUserId is null) return null;

            HttpContextAccessor httpContextAccessor = new();
            var userManager = httpContextAccessor
                .HttpContext
                .RequestServices
                .GetRequiredService<UserManager<UserApp>>();

            UserApp appUser = userManager.Users.First(p => p.Id == UpdateUserId);

            return appUser.Name + " " + appUser.SurName + " (" + appUser.Email + ")";
        }
        #endregion
    }
}
