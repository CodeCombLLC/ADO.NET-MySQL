using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySqlAspNetTest.Models;

namespace MySqlAspNetTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] MyContext DB)
        {
            var cnt = DB.Blogs.Count();
            return Content(cnt.ToString());
        }
    }
}
