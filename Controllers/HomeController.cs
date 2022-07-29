using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc_Auth.Models;

namespace Mvc_Auth.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index() => View(); 
        public IActionResult Privacy() => View();
    }
}
