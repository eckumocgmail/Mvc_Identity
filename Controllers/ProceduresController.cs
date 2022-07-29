using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;

namespace Mvc_IdentityApp.Controllers
{
    [Route("[controller]/[action]")]
    public class ProceduresController: Controller
    {
        public IActionResult Index() => Redirect("/Procedures/ActionList");
        public IActionResult List( ) => View(   );

        
    }
}
