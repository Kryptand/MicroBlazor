﻿using System.Threading.Tasks;
using Authentication.Models;
using AuthenticationService.DataClasses;

namespace AuthenticationService.Logic.Contracts
{
    public interface IUserService
    {
        Task<SecurityToken> AuthenticateAsync(string username, string password);
        Task<UserEntity> FindUserByUsernameAsync(string username);
        PasswordVerificationResult IsPasswordValid(UserEntity userEntity, string password);
        Task<UserEntity> RegisterUserAsync(string username, string password);
    }
}