﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace AuthenticationFrontend.Services
{
    public class UserService
    {
        private readonly HttpClient _http;
        [Inject]
        protected LocalStorage localStorage;

        public UserService(HttpClient http)
        {
            _http = http;
        }



        public bool IsAuthenticated()
        {
            var token = localStorage.GetItem<string>("token");

            return (token != null);
        }

        public string getToken()
        {
            return localStorage.GetItem<string>("token");
        }

        public void Clear()
        {
            localStorage.Clear();
        }


        // model.Email, model.Password, model.RememberMe, lockoutOnFailure: false
        public async Task<bool> PasswordSignInAsync(LoginViewModel model)
        {
            SearchInProgress = true;
            NotifyStateChanged();

            var result = await http.PostJsonAsync<Object>("/api/Account", model);

            if (result)// result.Succeeded
            {
                _logger.LogInformation("User logged in.");

                // Save the JWT token in the LocalStorage
                // https://github.com/BlazorExtensions/Storage
                await localStorage.SetItem<Object>("token", result);


                // Returns true to indicate the user has been logged in and the JWT token 
                // is saved on the user browser
                return true;

            }

        }
    }
}
// This is how you call your Web API, sending it the JWT token for // the current user