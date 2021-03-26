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
using Smidge;//Kütüphanemiz
using Smidge.Options;//Kütüphanemiz
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
                    .ForProduction(builder => //ForDebug da var eðer debug="true" kullanacaksak
                    builder
                    .EnableCompositeProcessing()
                    .EnableFileWatcher()//dosyalarda deðiþiklik olduðu zaman  0'dan cache oluþturacak aþaðýdaki sorgular yeniden cache üretme
                    .SetCacheBusterType<AppDomainLifetimeCacheBuster>()//bu da uygulama her ayaða kalktýðýnda silinip tekrar oluþturacak yani App_Data klasörünü
                    .CacheControlOptions(enableEtag: false, cacheControlMaxAge:0))
                    .Build());//istediðimiz kadar .js yolu verebiliriz, hepsini tek bir adýný verdiðimiz js yapar <script src="~/js/site.js" asp-append-version="true"></script>, <script src="~/js/site.js" asp-append-version="true"></script> yerine artýk istediðimiz adý verdiðimiz  <script src="my-js-bundle"></script> þeklinde kullanýrýz. ardýndan _ViewImports.cshtml kýsmýna @addTagHelper *,Smidge yazarýz görmesi için
                bundle.CreateCss("my-css_bundle", "~/css/site.css", "~/lib/bootstrap/dist/css/bootstrap.min.css");//js ile ayný mantýk , ile birden fazla ekleyebiliriz.
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
