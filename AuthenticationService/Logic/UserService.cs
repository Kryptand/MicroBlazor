using Authentication.Models;
using AuthenticationService.Data.Context;
using AuthenticationService.DataClasses;
using AuthenticationService.Helpers;
using AuthenticationService.Logic.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Logic
{

    public class UserService : IUserService
    {
        private readonly UserContext _userContext;
        private readonly AppSettings _appSettings;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IOptions<AppSettings> appSettings, UserContext userContext, IPasswordHasher passwordHasher)
        {
            _appSettings = appSettings.Value;
            _userContext = userContext;
            _passwordHasher = passwordHasher;
        }

        public Task<UserEntity> FindUserByUsernameAsync(string username)
        {
            return _userContext.UserEntities.SingleOrDefaultAsync(u => u.Username == username);
        }
        public PasswordVerificationResult IsPasswordValid(UserEntity userEntity, string password)
        {
            return _passwordHasher.VerifyHashedPassword(userEntity.Password, password);
        }
        public async Task<Authentication.Models.SecurityToken> AuthenticateAsync(string username, string password)
        {
            var user = await FindUserByUsernameAsync(username);
            if (user == null)
                return null;

            var credentialsValid = IsPasswordValid(user, password);
            if (credentialsValid == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(ClaimTypes.Name, user.Username),
                    new Claim("sub",user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            }; 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return new Authentication.Models.SecurityToken() { auth_token = jwtSecurityToken };
        }
        public async Task<UserEntity> RegisterUserAsync(string username, string password)
        {
            var userExists = await FindUserByUsernameAsync(username);
            if (userExists != null)
            {
                return null;
            }
            var userEntity = new UserEntity
            {
                Username = username,
                Password = _passwordHasher.HashPassword(password)
            };
            var addedUser= await _userContext.UserEntities.AddAsync(userEntity);
            await _userContext.SaveChangesAsync();
            return addedUser.Entity;
        }

    
    }
}