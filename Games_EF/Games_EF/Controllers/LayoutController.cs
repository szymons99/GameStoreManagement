using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Games_EF.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Layout", "true");
            return View();
        }
    }
}
