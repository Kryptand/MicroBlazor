using System.Threading.Tasks;
using Authentication.Models;

namespace AuthenticationFrontend.Services
{
    public interface IAuthenticationService
    {
        Task<PasswordVerificationResult> AuthenticateAsync(Login login);
        Task<string> GetTokenAsync();
        Task<bool> IsAuthenticatedAsync();
        Task LogoutAsync();
        Task WriteTokenAsync(SecurityToken token);
    }
}