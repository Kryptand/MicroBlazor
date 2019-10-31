using AuthenticationFrontend.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.ClientsideStorage;
using System.Collections.Generic;
using TG.Blazor.IndexedDB;

namespace AuthenticationFrontend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalDbStorage();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
