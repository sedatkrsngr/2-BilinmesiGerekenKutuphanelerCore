using ErrorHandlingApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;//Sonradan eklendi
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandlingApp.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]//Tüm herkes görsün
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]//hataların cashelenmemesi için konuldu. İstenirse konulmayabilir.
        public IActionResult ErrorPage()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();//Uygulamanın herhangi bir yerinde gelen hata verilerini yakalar

            CustomErrorModel model = new CustomErrorModel();
            model.hataYolu = exception.Path;
            model.Mesaj = exception.Error.Message;

            return View(model);
        }

        
        public IActionResult ErrorPage2()//Tüm sayfalarda kullanılması için shared altında olmalı
        {
            return View();
        }

        public IActionResult ErrorPage3()
        {
            return View();
        }
    }
}
