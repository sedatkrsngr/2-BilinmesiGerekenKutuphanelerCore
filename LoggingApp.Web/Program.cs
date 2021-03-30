using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();//ilk hali ama e�er buraya da loglama yapmak istiyorsak

            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Uygulama Aya�� Kalk�yor");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<Startup>();//ilk hali
                    webBuilder.UseStartup<Startup>().ConfigureLogging(log => {
                        //log.ClearProviders();//T�m logging providerlar� kald�r�r�z ve consolda g�r�nmez
                        //log.AddConsole();//Consolde �al��s�n dedik
                        //log.AddDebug();//Debugda �al���s�n dedik
                       
                    });
                });
    }
}
