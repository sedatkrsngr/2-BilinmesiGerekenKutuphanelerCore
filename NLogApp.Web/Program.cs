using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLogApp.Web
{
    public class Program
    {
        public static void Main(string[] args)//Bu tarafta loglama yapmayaca��m i�in buray� kullanm�cam. Detay i�in https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-5 sayfas�nda Nlog nas�l kurulur ad�mlar� var.
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(log =>
            {//bu k�s�mdan a�a��s� sonradan eklendi.
                log.ClearProviders();
                // log.SetMinimumLevel(LogLevel.Trace)// buras� nlog.config veya appsetting.json i�erisinde belli zaten veya hangi envoironmentteysek o appsettings.envirname.json da
            })
            .UseNLog();//Nlogu kullan demek
    }
}
