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
        public static void Main(string[] args)//Bu tarafta loglama yapmayacaðým için burayý kullanmýcam. Detay için https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-5 sayfasýnda Nlog nasýl kurulur adýmlarý var.
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
            {//bu kýsýmdan aþaðýsý sonradan eklendi.
                log.ClearProviders();
                // log.SetMinimumLevel(LogLevel.Trace)// burasý nlog.config veya appsetting.json içerisinde belli zaten veya hangi envoironmentteysek o appsettings.envirname.json da
            })
            .UseNLog();//Nlogu kullan demek
    }
}
