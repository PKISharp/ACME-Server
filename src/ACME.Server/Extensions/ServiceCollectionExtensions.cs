using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using System.Linq;
using TGIT.ACME.Protocol.HttpModel.Converters;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Protocol.Storage;
using TGIT.ACME.Protocol.Storage.FileStore;
using TGIT.ACME.Protocol.Storage.FileStore.Configuration;
using TGIT.ACME.Protocol.Workers;
using TGIT.ACME.Server.BackgroundServices;
using TGIT.ACME.Server.Configuration;
using TGIT.ACME.Server.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddACMEServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRequestValidationService, DefaultRequestValidationService>();
            services.AddScoped<INonceService, DefaultNonceService>();
            services.AddScoped<IAccountService, DefaultAccountService>();
            services.AddScoped<IOrderService, DefaultOrderService>();

            services.AddScoped<IIssuanceWorker, IssuanceWorker>();
            services.AddScoped<IValidationWorker, ValidationWorker>();

            services.AddScoped<AddNextNonceFilter>();

            services.AddHostedService<HostedValidationService>();
            services.AddHostedService<HostedIssuanceService>();

            services.Configure<MvcOptions>(opt =>
            {
                opt.Filters.Add(typeof(AcmeExceptionFilter));
                opt.Filters.Add(typeof(ValidateAcmeHeaderFilter));
                opt.Filters.Add(typeof(ValidateNonceFilter));
                opt.Filters.Add(typeof(ValidateSignatureFilter));
                opt.Filters.Add(typeof(AcmeIndexLinkFilter));

                var jsonConverters = opt.InputFormatters
                    .OfType<SystemTextJsonInputFormatter>()
                    .First()
                    .SerializerOptions
                    .Converters;

                jsonConverters.Add(new AcmeJsonConverterFactory());
            });

            var acmeServerConfig = configuration.GetSection("AcmeServer");
            var acmeServerOptions = new ACMEServerOptions();
            acmeServerConfig.Bind(acmeServerOptions);

            services.Configure<ACMEServerOptions>(acmeServerConfig);

            return services;
        }

        public static IServiceCollection AddACMEFileStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INonceStore, NonceStore>();
            services.AddScoped<IAccountStore, AccountStore>();
            services.AddScoped<IOrderStore, OrderStore>();

            services.Configure<FileStoreOptions>(configuration.GetSection("FileStore"));

            return services;
        }
    }
}
