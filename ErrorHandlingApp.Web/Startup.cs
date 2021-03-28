using ErrorHandlingApp.Web.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandlingApp.Web
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
            services.AddControllersWithViews();
            //services.AddControllersWithViews(opt=> {

            //    opt.Filters.Add(new CustomHandleExceptionFilterAttribute() { ErrorPage="ErrorPage2" }); // eðer tek bir hata sayfasý yapmak istiyorsak ve her controller baþýna fiter eklemek istemiyorsak services.AddControllersWithViews(); açarak her hatada ErrorPage2 Sayfasýný çaðýracak metodu böyle yazarýz
            //});
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())//Development ortamýndayken
            {
                //app.UseDeveloperExceptionPage();//Detaylý hata gösterimi
               
            }
            else
            {
                //app.UseExceptionHandler("/Error/ErrorPage");//Kendi belirlediðimiz hata sayfasý Development dýþýnda Production ve Staging ortamda çalýþýr. Bunun en güzel özelliði verdiðimiz hata sayfasý gittiðimiz linkte görünüyor yani sayfayý yenilediðinde /Error/ErrorPage deðil de gittiðimiz link yenilenir
                app.UseHsts();
            }

            app.UseStatusCodePages();//Varsayýlan olarak status hatalarýný döner 400-599 arasýndaki hatalarý döner
            //app.UseDatabaseErrorPage();//Veritabanýna baðlanýrken alýnan hatalarý falan detaylý gösterir
           // app.UseStatusCodePages("text/plain","Bir Hata Gerceklesti. Durum Kodu : {0}");// Bunu eklediðimizde bizden kaynaklý olmayan hatalarý da biz status code olarak döneriz. Örn sayfa bulunamadý 404
            //app.UseStatusCodePages( async options => { Bir baþka yol. Ýstersek Content type html belirtip manuel status hata sayfasý dönderebiliriz

            //    options.HttpContext.Response.ContentType = "text/plain";
            //    await options.HttpContext.Response.WriteAsync($"Bir Hata Gerceklesti. Durum Kodu :{options.HttpContext.Response.StatusCode}");
            //});


            //  app.UseExceptionHandler("/Error/ErrorPage"); Test amaçlý buraya koydum ExceptionFilter kullanacaðým için Development tarafýný da yorum yaptým. Eðer Filter kullanmazsak bunu istediðimiz yerde kullanýrýz

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
