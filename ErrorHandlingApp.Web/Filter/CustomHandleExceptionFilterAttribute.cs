using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandlingApp.Web.Filter
{
    public class CustomHandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public string ErrorPage { get; set; }
        public override void OnException(ExceptionContext context)//override yazınca kalıtım aldığı virtualları gelir
        {
            var result = new ViewResult { ViewName = ErrorPage };
            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(),context.ModelState);
            result.ViewData.Add("Exception",context.Exception.Message);
            result.ViewData.Add("Url", context.HttpContext.Request.Path.Value);

            context.Result = result;
        }
    }
}
