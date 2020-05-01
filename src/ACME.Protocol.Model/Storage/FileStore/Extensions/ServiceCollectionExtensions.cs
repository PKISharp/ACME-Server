using ACME.Server.Services;
using ACME.Server.Storage;
using ACME.Server.Store.Filebased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddACMEFileStore(this IServiceCollection services)
        {
            services.AddScoped<INonceStore, NonceStore>();
            services.AddScoped<IAccountStore, AccountStore>();
            services.AddScoped<IOrderStore, AccountStore>();

            return services;
        }
    }
}
