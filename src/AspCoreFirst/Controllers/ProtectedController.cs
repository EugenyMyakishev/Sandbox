﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspCoreFirst.Controllers
{
    [Authorize]
    public class ProtectedController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Protected()
        {
            return View();
        }
    }
}
