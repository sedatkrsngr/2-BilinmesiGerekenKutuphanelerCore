using HangfireApp.Web.BackgroundJobs;
using HangfireApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

   

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()//Örn üye kaydoluyor
        {
            FireAndForgetJobs.EmailSendToUserJob("125", "Hoşgeldiniz");//kullanıcının id sini falan aldık ve esajı gönderdik diyelim kontrolü için /hangfire bakabiliriz

            string JobID = DelayedJobs.EmailSendToUserJob("126", "Hoşgeldiniz");//20 sn sonra çalışsın dedik

            RecurringJobs.EmailSendToUserJob("127", "Hoşgeldiniz");

            ContinuationsJobs.EmailSendToUserJob(JobID, "Çalıştırıldı");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
