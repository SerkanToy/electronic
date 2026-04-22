using electronic.Application.Abstracts;
using electronic.Domain.Requests;
using electronik.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace electronic.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAuthTokenProcessor authTokenProcessor;
        private readonly UserManager<UserApp> userManager;
        private readonly SignInManager<UserApp> signInManager;

        public AccountService()
        {
            
        }

        public Task LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task RefreshTokenAsync(string? refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task RegisterAsync(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
