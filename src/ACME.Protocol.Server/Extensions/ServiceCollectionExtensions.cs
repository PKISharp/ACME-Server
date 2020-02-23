using ACME.Protocol.Server.Filters;
using ACME.Protocol.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddACMEServer(this IServiceCollection services)
        {
            services.AddScoped<INonceService, DefaultNonceService>();
            services.AddScoped<IAccountService, DefaultAccountService>();
            services.AddScoped<IRequestValidationService, DefaultRequestValidationService>();

            services.AddScoped<AddNextNonceFilter>();

            services.Configure<MvcOptions>(opt =>
            {
                opt.Filters.Add(typeof(AcmeExceptionFilter));
                opt.Filters.Add(typeof(ValidateAcmeHeaderFilter));
                opt.Filters.Add(typeof(ValidateNonceFilter));
                opt.Filters.Add(typeof(ValidateSignatureFilter));
                opt.Filters.Add(typeof(AcmeIndexLinkFilter));
            });

            return services;
        }
    }
}
