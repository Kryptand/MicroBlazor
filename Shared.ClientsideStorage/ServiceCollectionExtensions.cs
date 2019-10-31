using Microsoft.Extensions.DependencyInjection;
using Shared.ClientsideStorage.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using TG.Blazor.IndexedDB;
namespace Shared.ClientsideStorage
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIndexedDbStorage(this IServiceCollection services, Action<DbStore> options) {
            services.AddTransient<IStorageService, StorageService>();
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
