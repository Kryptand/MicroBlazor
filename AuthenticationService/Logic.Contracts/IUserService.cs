using System.Threading.Tasks;
using AuthenticationService.DataClasses;
using AuthenticationService.Models;

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