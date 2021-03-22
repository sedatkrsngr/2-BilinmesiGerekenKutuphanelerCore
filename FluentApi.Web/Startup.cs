using FluentApi.Web.FluentValidators;
using FluentApi.Web.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Context tanýtýlýr, biz ekledik 
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConStr"]);//appsettings.json içerisinde
            });

            //services.AddControllersWithViews();// Önceki varsayýlan ve þimdi bir sonraki gibi oldu Fluent için
            //services.AddSingleton<IValidator<Customer>, CustomerValidator>();
            //Aþaðýda AbstractValidator sayesinde kalýtým alarak IValidator çaðýrmýþ tüm hepsini tek seferde hallederken. 2.Yol olarak yukarýdaki gibi tek tek de ekleyebiliriz.
            //Mantýklý olan aþaðýdaki
            services.AddControllersWithViews().AddFluentValidation(options => {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {

                options.SuppressModelStateInvalidFilter = true;  //Api oluþturduðumuzda artýk bizim yaptýðýmýz validator mesajýný dönmek için yaparýz.

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
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
