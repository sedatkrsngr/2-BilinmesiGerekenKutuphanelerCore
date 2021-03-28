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

            //    opt.Filters.Add(new CustomHandleExceptionFilterAttribute() { ErrorPage="ErrorPage2" }); // e�er tek bir hata sayfas� yapmak istiyorsak ve her controller ba��na fiter eklemek istemiyorsak services.AddControllersWithViews(); a�arak her hatada ErrorPage2 Sayfas�n� �a��racak metodu b�yle yazar�z
            //});
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())//Development ortam�ndayken
            {
                //app.UseDeveloperExceptionPage();//Detayl� hata g�sterimi
               
            }
            else
            {
                //app.UseExceptionHandler("/Error/ErrorPage");//Kendi belirledi�imiz hata sayfas� Development d���nda Production ve Staging ortamda �al���r. Bunun en g�zel �zelli�i verdi�imiz hata sayfas� gitti�imiz linkte g�r�n�yor yani sayfay� yeniledi�inde /Error/ErrorPage de�il de gitti�imiz link yenilenir
                app.UseHsts();
            }

            app.UseStatusCodePages();//Varsay�lan olarak status hatalar�n� d�ner 400-599 aras�ndaki hatalar� d�ner
            //app.UseDatabaseErrorPage();//Veritaban�na ba�lan�rken al�nan hatalar� falan detayl� g�sterir
           // app.UseStatusCodePages("text/plain","Bir Hata Gerceklesti. Durum Kodu : {0}");// Bunu ekledi�imizde bizden kaynakl� olmayan hatalar� da biz status code olarak d�neriz. �rn sayfa bulunamad� 404
            //app.UseStatusCodePages( async options => { Bir ba�ka yol. �stersek Content type html belirtip manuel status hata sayfas� d�nderebiliriz

            //    options.HttpContext.Response.ContentType = "text/plain";
            //    await options.HttpContext.Response.WriteAsync($"Bir Hata Gerceklesti. Durum Kodu :{options.HttpContext.Response.StatusCode}");
            //});


            //  app.UseExceptionHandler("/Error/ErrorPage"); Test ama�l� buraya koydum ExceptionFilter kullanaca��m i�in Development taraf�n� da yorum yapt�m. E�er Filter kullanmazsak bunu istedi�imiz yerde kullan�r�z

            
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
