using Microsoft.AspNetCore.Mvc;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Server.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddACMEServer(this IServiceCollection services)
        {
            services.AddScoped<IRequestValidationService, DefaultRequestValidationService>();
            services.AddScoped<INonceService, DefaultNonceService>();
            services.AddScoped<IAccountService, DefaultAccountService>();
            services.AddScoped<IOrderService, DefaultOrderService>();

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
