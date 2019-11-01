using Authentication.Models;
using System.Threading.Tasks;

namespace AuthenticationFrontend.Services
{
    public interface IAuthenticationService
    {
        Task<PasswordVerificationResult> AuthenticateAsync(Login login);
        Task<SecurityToken> GetTokenAsync();
        Task<bool> IsAuthenticatedAsync();
        Task LogoutAsync();
        Task WriteTokenAsync(SecurityToken token);
    }
}