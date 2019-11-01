using Authentication.Models;
using Microsoft.AspNetCore.Components;
using Shared.ClientsideStorage.Logic;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthenticationFrontend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public const string TOKEN_STORAGE_IDENTIFIER = "token";
        private const string TABLE_STORAGE_IDENTIFIER = "authentication";
        private const string AUTHENTICATION_URI = "http://localhost:5000/api/user/authenticate";

        private readonly HttpClient _http;
        private readonly IStorageService _storageService;

        public AuthenticationService(HttpClient http, IStorageService storageService)
        {
            _http = http;
            _storageService = storageService;
        }
        public async Task<PasswordVerificationResult> AuthenticateAsync(Login login)
        {

            try
            {
                var token = await _http.PostJsonAsync<SecurityToken>(AUTHENTICATION_URI, login);
                await WriteTokenAsync(token);
                return PasswordVerificationResult.Success;
            }
            catch (Exception e)
            {
                return PasswordVerificationResult.Failed;
            }




        }
        public async Task LogoutAsync()
        {
            await _storageService.DeleteAsync(TABLE_STORAGE_IDENTIFIER, TOKEN_STORAGE_IDENTIFIER);
        }
        public async Task<SecurityToken> GetTokenAsync()
        {
            return await _storageService.GetSingleAsync<string, SecurityToken>(TABLE_STORAGE_IDENTIFIER, TOKEN_STORAGE_IDENTIFIER);
        }

        public async Task WriteTokenAsync(SecurityToken token)
        {
            await _storageService.AddAsync<SecurityToken>(TABLE_STORAGE_IDENTIFIER, token);
        }
        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _storageService.GetSingleAsync<string, SecurityToken>(TABLE_STORAGE_IDENTIFIER, TOKEN_STORAGE_IDENTIFIER);
            return (token != null);
        }
    }
}
