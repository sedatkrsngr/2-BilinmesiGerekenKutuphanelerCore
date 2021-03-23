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
            //Ip veya Client hangisi kullan�lacaksa yorum sat�r�ndaki o kodlar kullan�lmal�. �kisi birden kullan�l�r m� denemek laz�m
            //RATEL�M�T Sonradan Eklendi
            services.AddOptions();//appsettings.json i�erisinde bulunan key,value de�erlerini bir class �zerinden okuma i�lemi i�in eklendi.
            services.AddMemoryCache();//ka� istek yap�ld���n� falan sunucunun raminde tutmak i�in eklendi.
            //********************************
            //Ip ile k�s�tlama
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));    //appsettings.json i�erisindeki key ile value de�erini elde ederiz
            services.Configure<IpRateLimitPolicy>(Configuration.GetSection("IpRateLimitPolicies"));//appsettings.json i�erisindeki key ile value de�erini elde ederiz farkl� ipleri tutucaz
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();     // Yukar�daki IpRateLimitPolicy  key,value de�erlerini memoryde kullanmak i�in bunu kullan�r�z tek sunucuda �al���yorsak yoksa DistributedCacheIpPolicyStore 
            //Ip ile k�s�tlama

            //*********************************

            //Client ile k�s�tlama 
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));
            services.Configure<ClientRateLimitPolicy>(Configuration.GetSection("ClientRateLimitPolicies"));
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            //Client ile k�s�tlama

            //**********************************

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();// Yukar�daki IpRateLimitOptions  key,value de�erlerini memoryde kullanmak i�in bunu kullan�r�z tek sunucuda �al���yorsak yoksa DistributedCacheRateLimitCounterStore
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//Request yapan�n ip ,header bilgisini okuyabilmek i�in eklenir.
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();//Ana servis
            //RATEL�M�T Sonradan Eklendi



            services.AddControllers();
        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //RATEL�M�T Sonradan Eklendi
            //Ip i�im
            app.UseIpRateLimiting();
            //Ip i�im

            //Client i�in
            app.UseClientRateLimiting();
            //Client i�in
            //RATEL�M�T Sonradan Eklendi
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
