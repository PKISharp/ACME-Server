using ACME.Protocol.Server.Filters;
using ACME.Protocol.Services;
using ACME.Protocol.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddACMEServer(this IServiceCollection services)
        {
            services.AddScoped<INonceService, DefaultNonceService>();

            services.AddScoped<AddNextNonceFilter>();

            services.Configure<MvcOptions>(opt =>
            {
                opt.Filters.Add(typeof(AcmeExceptionFilter));
                opt.Filters.Add(typeof(AcmeIndexLinkFilter));
            });

            return services;
        }
    }
}
