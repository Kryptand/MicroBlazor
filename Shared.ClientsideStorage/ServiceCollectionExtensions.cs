using Microsoft.Extensions.DependencyInjection;
using Shared.ClientsideStorage.Logic;
using System;
using TG.Blazor.IndexedDB;
namespace Shared.ClientsideStorage
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIndexedDbStorage(this IServiceCollection services, Action<DbStore> options)
        {
            services.AddTransient<IStorageService, IndexedDbStorageService>();
            services.AddIndexedDB(options);
            return services;
        }
        public static IServiceCollection AddLocalDbStorage(this IServiceCollection services)
        {
            services.AddTransient<IStorageService, LocalStorageService>();
            return services;
        }
    }
}
