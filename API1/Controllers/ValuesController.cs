﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        public IActionResult Index()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
        
        [HttpGet("names")]
        public IActionResult Names()
        {
            return new JsonResult("testNames");
        }
    }
}