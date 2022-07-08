using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TesteCulture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("pt-BR"),
                new CultureInfo("en-US")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/html";

                var detectedCultureName = CultureInfo.CurrentCulture.DisplayName;
                var detectedUiCultureName = CultureInfo.CurrentUICulture.DisplayName;

                var cultureTable =
                    "<html><body>" +
                        "<table border=1 cellpadding=10>" +
                            $"<tr><td>Culture:</td><td>{detectedCultureName}</td></tr>" +
                            $"<tr><td>UI Culture:</td><td>{detectedUiCultureName}</td></tr>" +
                            $"<tr><td>Today:</td><td>{DateTime.Now}</td></tr>" +                            
                            $"<tr><td>Currency:</td><td>{(1599):C}</td></tr>" +
                        "</table>" +
                    "</body></html>";

                await context.Response.WriteAsync(cultureTable);
            });
        }
    }
}
