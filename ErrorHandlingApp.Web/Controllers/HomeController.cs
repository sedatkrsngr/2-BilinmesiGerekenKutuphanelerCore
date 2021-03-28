using ErrorHandlingApp.Web.Filter;
using ErrorHandlingApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandlingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [CustomHandleExceptionFilter(ErrorPage ="ErrorPage2")]// Bu şekilde controllerda hata varsa belirli bir sayfaya yönlendirebiliriz. En tepeye koyarsak hepsi bu sayfada çalışacaktır
        public IActionResult Index()
        {
            int a = 5, b = 0, sonuc;
            sonuc = a / b;
            return View();
        }

        [CustomHandleExceptionFilter(ErrorPage = "ErrorPage3")]
        public IActionResult Privacy()
        {
            int a = 5, b = 0, sonuc;
            sonuc = a / b;
            return View();
        }

    }
}
