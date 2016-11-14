using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspCoreFirst.Context;
using AspCoreFirst.Models;
using AspCoreFirst.Models.EntityHelpers;
using AspCoreFirst.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using AspCoreFirst.Filters;
using AspCoreFirst.Dummy;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspCoreFirst.Controllers
{
   [LanguageActionFilter]
    public class AngularController : Microsoft.AspNetCore.Mvc.Controller
    {

        private MyDbContext Context { get; set; }

        public AngularController(MyDbContext context)
        {
            this.Context = context;
        }
        // GET: /<controller>/
        public IActionResult Index([FromServices]IService service)
        {
            var list = new List<Players>() {new Players()
            { Age = 33, Club = "Barcelona", Name = "Neymar", Id = 0},
            new Players()
            { Age = 22, Club = "Juventus", Name = "Tevez", Id = 1},
             new Players()
            { Age = 21, Club = "Manchester United", Name = "Suarez", Id = 2},
            };
            return View(list);
        }
        [HttpGet]
        public JsonResult GetResult([FromServices]IService service)
        {
            var list = new List<Players>() {new Players()
            { Age = 33, Club = "Barcelona", Name = "Neymar", Id = 0},
            new Players()
            { Age = 22, Club = "Juventus", Name = "Tevez", Id = 1},
             new Players()
            { Age = 21, Club = "Manchester United", Name = "Suarez", Id = 2},
            };
            return Json(list);
        }
    }

    public class Players
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Age { get; set; }
        public string Club { get; set; }
    }
}
