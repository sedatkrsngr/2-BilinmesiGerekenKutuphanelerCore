using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smidge;//K�t�phanemiz
using Smidge.Options;//K�t�phanemiz
using Smidge.Cache;

namespace SmidgeApp.Web
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
            services.AddSmidge(Configuration.GetSection("smidge"));//Sonradan eklendi

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSmidge(bundle =>//sonradan eklendi
            {
                bundle.CreateJs("my-js-bundle", "~/js/").WithEnvironmentOptions(BundleEnvironmentOptions
                    .Create()
                    .ForProduction(builder => //ForDebug da var e�er debug="true" kullanacaksak
                    builder
                    .EnableCompositeProcessing()
                    .EnableFileWatcher()//dosyalarda de�i�iklik oldu�u zaman  0'dan cache olu�turacak a�a��daki sorgular yeniden cache �retme
                    .SetCacheBusterType<AppDomainLifetimeCacheBuster>()//bu da uygulama her aya�a kalkt���nda silinip tekrar olu�turacak yani App_Data klas�r�n�
                    .CacheControlOptions(enableEtag: false, cacheControlMaxAge:0))
                    .Build());//istedi�imiz kadar .js yolu verebiliriz, hepsini tek bir ad�n� verdi�imiz js yapar <script src="~/js/site.js" asp-append-version="true"></script>, <script src="~/js/site.js" asp-append-version="true"></script> yerine art�k istedi�imiz ad� verdi�imiz  <script src="my-js-bundle"></script> �eklinde kullan�r�z. ard�ndan _ViewImports.cshtml k�sm�na @addTagHelper *,Smidge yazar�z g�rmesi i�in
                bundle.CreateCss("my-css_bundle", "~/css/site.css", "~/lib/bootstrap/dist/css/bootstrap.min.css");//js ile ayn� mant�k , ile birden fazla ekleyebiliriz.
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
