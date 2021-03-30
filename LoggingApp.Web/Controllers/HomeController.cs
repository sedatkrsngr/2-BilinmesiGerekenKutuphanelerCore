using LoggingApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;//Varsayılan geliyor yoksa biz ekleyebiliriz
        private readonly ILoggerFactory _loggerFactory;// Ilogger gibi controller ismini değil de kendi tanımladığımız ismi kullanmak için kullanabiliriz

        public HomeController(ILogger<HomeController> logger,ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
        }

        public IActionResult Index()
        {
            //Yukarıdan aşağıya düşükten yükseğe doğru kritik seviyelerdir
            //İstersek Program.js içerisinde consolda hiç birinin görünmemesini sağlayabiliriz
            //appsettings.json içerisinde logging taginde bulunan Default: alan ne ise o ve ondan sonrakiler görünür. Öncekiler görünmez.
            //Farklı enviriontmentlerde çalışırsak onların appsetting.enviriontmentname.json larında bu tanımlamayı yapabiliriz
           
            _logger.LogTrace("Index Sayfasına Girildi.");//Consolda görünmedi appsettings.json içerisinde varsayılan Information altı olduğu için
            _logger.LogDebug("Index Sayfasına Girildi.");//Consolda görünmedi appsettings.json içerisinde varsayılan Information altı olduğu için

            _logger.LogInformation("Index Sayfasına Girildi.");
            _logger.LogWarning("Index Sayfasına Girildi.");
            _logger.LogError("Index Sayfasına Girildi.");
            _logger.LogCritical("Index Sayfasına Girildi.");

            //Consolde görünmeyen uyarılar
            //0-//Trace
            //1-//Debug
            //Consolda görünen uyarılar
            //2//Information: Index Sayfasına Girildi.
            //3//Warning: Index Sayfasına Girildi.
            //4//Error: Index Sayfasına Girildi.
            //5//Critical: Index Sayfasına Girildi.

            var log = _loggerFactory.CreateLogger("HomeSınıfı");//kendi verdiğimiz isimle loglama yapabiliyoruz illa controller adı vermeye gerek yok

            log.LogTrace("Index Sayfasına Girildi.");
            log.LogDebug("Index Sayfasına Girildi.");

            log.LogInformation("Index Sayfasına Girildi.");
            log.LogWarning("Index Sayfasına Girildi.");
            log.LogError("Index Sayfasına Girildi.");
            log.LogCritical("Index Sayfasına Girildi.");



            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
