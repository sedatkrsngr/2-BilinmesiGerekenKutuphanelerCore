using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimit.Api
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
            //Ip veya Client hangisi kullanýlacaksa yorum satýrýndaki o kodlar kullanýlmalý. Ýkisi birden kullanýlýr mý denemek lazým
            //RATELÝMÝT Sonradan Eklendi
            services.AddOptions();//appsettings.json içerisinde bulunan key,value deðerlerini bir class üzerinden okuma iþlemi için eklendi.
            services.AddMemoryCache();//kaç istek yapýldýðýný falan sunucunun raminde tutmak için eklendi.
            //********************************
            //Ip ile kýsýtlama
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));    //appsettings.json içerisindeki key ile value deðerini elde ederiz
            services.Configure<IpRateLimitPolicy>(Configuration.GetSection("IpRateLimitPolicies"));//appsettings.json içerisindeki key ile value deðerini elde ederiz farklý ipleri tutucaz
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();     // Yukarýdaki IpRateLimitPolicy  key,value deðerlerini memoryde kullanmak için bunu kullanýrýz tek sunucuda çalýþýyorsak yoksa DistributedCacheIpPolicyStore 
            //Ip ile kýsýtlama

            //*********************************

            //Client ile kýsýtlama 
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));
            services.Configure<ClientRateLimitPolicy>(Configuration.GetSection("ClientRateLimitPolicies"));
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            //Client ile kýsýtlama

            //**********************************

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();// Yukarýdaki IpRateLimitOptions  key,value deðerlerini memoryde kullanmak için bunu kullanýrýz tek sunucuda çalýþýyorsak yoksa DistributedCacheRateLimitCounterStore
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//Request yapanýn ip ,header bilgisini okuyabilmek için eklenir.
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();//Ana servis
            //RATELÝMÝT Sonradan Eklendi



            services.AddControllers();
        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //RATELÝMÝT Sonradan Eklendi
            //Ip içim
            app.UseIpRateLimiting();
            //Ip içim

            //Client için
            app.UseClientRateLimiting();
            //Client için
            //RATELÝMÝT Sonradan Eklendi
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
