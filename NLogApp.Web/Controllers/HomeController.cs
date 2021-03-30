using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLogApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NLogApp.Web.Controllers
{
    //Detay için https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-5 sayfasında Nlog nasıl kurulur adımları var.
    //Sitede Mail,Veritabanı,dosya,consol gibi hedef için eğitimler var

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int a = 5,b=0,c;
            try
            {
                c = a / b;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);              
            }

            _logger.LogInformation("Index Başlatıldı");
            _logger.LogWarning("Index Başlatıldı");
            _logger.LogCritical("Index Başlatıldı");
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
