using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    public class ValuesController : Controller
    {
        public IActionResult Index()
        {
            return new JsonResult("test");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Names()
        {
            return new JsonResult("test");
        }
    }
}