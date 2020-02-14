using ACME.Protocol.Services;
using ACME.Protocol.Storage;
using ACME.Protocol.Store.Filebased;
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

            return services;
        }
    }
}
