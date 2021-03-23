using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(new { Id = 1, Category= "Kırtasiye" });
        }
    }
}
