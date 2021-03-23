using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;//SonradanEklendi GetRequiredService için
using AspNetCoreRateLimit;////SonradanEklendi  IIpPolicyStore için

namespace RateLimit.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();//eski hali 
            //Client ve Ip birlikte kullanýlabilir mi bilmiyorum. Eðer kullanýlmazsa appsetting.js dosyasý içerisinde Ip veya client tek eklenir
            var webhost = CreateHostBuilder(args).Build();

            //Ip sýnýrlama için
            var IpPolicy = webhost.Services.GetRequiredService<IIpPolicyStore>();//Services.GetService geriye null dönerken  GetRequiredService hata döner bu yüzden bunu kullandýk
            IpPolicy.SeedAsync().Wait();//sonuc gelene kadar bekliyor
            //Ip sýnýrlama için

            //Client sýnýrlama için
            var ClientPolicy = webhost.Services.GetRequiredService<IClientPolicyStore>();//Services.GetService geriye null dönerken  GetRequiredService hata döner bu yüzden bunu kullandýk           
            ClientPolicy.SeedAsync().Wait();//sonuc gelene kadar bekliyor
            //Client sýnýrlama için

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
