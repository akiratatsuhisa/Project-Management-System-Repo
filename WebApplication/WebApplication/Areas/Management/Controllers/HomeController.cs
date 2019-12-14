using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}