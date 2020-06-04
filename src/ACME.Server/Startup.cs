using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using TGIT.ACME.Protocol.HttpModel.Converters;
using TGIT.ACME.Protocol.Storage.FileStore.Configuration;
using TGIT.ACME.Server.Configuration;

namespace TGIT.ACME.Server
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(mvc =>
            {
                var jsonConverters = mvc.InputFormatters
                    .OfType<SystemTextJsonInputFormatter>()
                    .First()
                    .SerializerOptions
                    .Converters;

                jsonConverters.Add(new AcmeJsonConverterFactory());
            });
            services.AddACMEServer();
            services.AddACMEFileStore();

            var acmeServerConfig = _configuration.GetSection("AcmeServer");
            var acmeServerOptions = new ACMEServerOptions();
            acmeServerConfig.Bind(acmeServerOptions);

            services.Configure<ACMEServerOptions>(acmeServerConfig);
            services.Configure<FileStoreOptions>(_configuration.GetSection("FileStore"));

            if (acmeServerOptions.UseHostedServices) {
                //TODO: Register Challenge Validation
                //TODO: Register Certificate Issuance
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
