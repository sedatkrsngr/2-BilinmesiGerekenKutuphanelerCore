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
using Hangfire;//Sonradan eklendi
using HangfireApp.Web.Services;

namespace HangfireApp.Web
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
            services.AddScoped<IEmailSender, MailSender>();//IEmailSender G�rd���nde MailSender nesnesi �retiyoruz
            services.AddHangfire(con => con.UseSqlServerStorage(Configuration.GetConnectionString("HangFireConStr")));//Hangfire yolunu verdik. Joblar buraya kaydolacak uygulama ilk kez aya�a kalkt���nda tablolar veritaban�nda olu�ur
            services.AddHangfireServer();//Hangfire �al��t�rd�k


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

            app.UseHangfireDashboard("/hangfire");//dashboard tan�mlad�k. Sitead�/verdi�imiz ad burada mesela sitead�/hangfire diyince dashboard g�r�necek Routing �ncesinde tan�mlanmal� burada joblar,tekrar eden i�lemler,Yinelenen joblar ve Server ad� var

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
