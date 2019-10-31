using System;
using System.Net.Http;
using System.Threading.Tasks;
using Authentication.Models;
using Microsoft.AspNetCore.Components;
using Shared.ClientsideStorage.Logic;

  namespace AuthenticationFrontend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public const string TOKEN_STORAGE_IDENTIFIER = "token";
        private const string TABLE_STORAGE_IDENTIFIER = "authentication";
        private const string AUTHENTICATION_URI = "/api/user/authenticate";

        private readonly HttpClient _http;
        private readonly IStorageService _storageService;

        public AuthenticationService(HttpClient http, IStorageService storageService)
        {
            _http = http;
            _storageService = storageService;
        }
        public async Task<PasswordVerificationResult> AuthenticateAsync(Login login)
        {
            var token = await _http.PostJsonAsync<SecurityToken>(AUTHENTICATION_URI, login);
            if (token == null)
            {
                return PasswordVerificationResult.Failed;
            }
            await WriteTokenAsync(token);
            return PasswordVerificationResult.Success;
        }
        public async Task LogoutAsync()
        {
            await _storageService.DeleteAsync(TABLE_STORAGE_IDENTIFIER,TOKEN_STORAGE_IDENTIFIER);
        }
        public async Task<string> GetTokenAsync()
        {
            return await _storageService.GetSingleAsync<string,string>(TABLE_STORAGE_IDENTIFIER,TOKEN_STORAGE_IDENTIFIER);
        }

        public async Task WriteTokenAsync(SecurityToken token)
        { 
            await _storageService.AddAsync<SecurityToken>(TABLE_STORAGE_IDENTIFIER,token);
        }
        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _storageService.GetSingleAsync<string,string>(TABLE_STORAGE_IDENTIFIER,TOKEN_STORAGE_IDENTIFIER);
            return (token != null);
        }
    }
}
