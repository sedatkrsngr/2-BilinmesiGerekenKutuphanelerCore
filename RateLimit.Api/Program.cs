using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;//SonradanEklendi GetRequiredService i�in
using AspNetCoreRateLimit;////SonradanEklendi  IIpPolicyStore i�in

namespace RateLimit.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();//eski hali 
            //Client ve Ip birlikte kullan�labilir mi bilmiyorum. E�er kullan�lmazsa appsetting.js dosyas� i�erisinde Ip veya client tek eklenir
            var webhost = CreateHostBuilder(args).Build();

            //Ip s�n�rlama i�in
            var IpPolicy = webhost.Services.GetRequiredService<IIpPolicyStore>();//Services.GetService geriye null d�nerken  GetRequiredService hata d�ner bu y�zden bunu kulland�k
            IpPolicy.SeedAsync().Wait();//sonuc gelene kadar bekliyor
            //Ip s�n�rlama i�in

            //Client s�n�rlama i�in
            var ClientPolicy = webhost.Services.GetRequiredService<IClientPolicyStore>();//Services.GetService geriye null d�nerken  GetRequiredService hata d�ner bu y�zden bunu kulland�k           
            ClientPolicy.SeedAsync().Wait();//sonuc gelene kadar bekliyor
            //Client s�n�rlama i�in

            webhost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
