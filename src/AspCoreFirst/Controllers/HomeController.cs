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
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signinManager;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<Shared> _htmlLocalizer;


        private MyDbContext Context { get; set; }

        public HomeController(MyDbContext context, UserManager<ApplicationUser> user, IEmailService emailService, IStringLocalizer<Shared> localizer)
        {
            _htmlLocalizer = localizer;
            _userManager = user;
            this._emailService = emailService;
            this.Context = context;
        }
        // GET: /<controller>/
        public IActionResult Index([FromServices]IService service)
        {
            ViewData["str"] = _htmlLocalizer["Hello"];
            ViewData["Say"] = service.Say();
            return View(new UserLoginHelper());
        }

        public IActionResult Res()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromServices] SignInManager<ApplicationUser> signInManager, UserLoginHelper author)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            this._signinManager = signInManager;
            var newUser  = new ApplicationUser() { Email = author.Email, UserName = author.Email };
              var res = await _userManager.CreateAsync(newUser, author.Password);
            //var str = _userManager.GetValidTwoFactorProvidersAsync(newUser);
            if (res.Succeeded)
            {
                // await _signinManager.SignInAsync(newUser, false);
                var code = _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var url = Url.Action("ConfirmEmail", new { userId = newUser.Id, code = code.Result });
                await _emailService.SendAsync(newUser.Email, "Confirm Your email", $"<a href = '{url}'>Confirm</a>");
                //return RedirectToAction("Protected", "Protected");
            }
                return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var res = await _userManager.ConfirmEmailAsync(user, code);
            if(res.Succeeded)
                return RedirectToAction("Protected", "Protected");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromServices] SignInManager<ApplicationUser> signInManager, UserLoginHelper author)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            this._signinManager = signInManager;
            var newUser = await _userManager.FindByNameAsync(author.Email);
            if(newUser==null) return RedirectToAction("Index");
            var res = await _signinManager.PasswordSignInAsync(newUser, author.Password, false,false);
            if(res.Succeeded)
                return RedirectToAction("Protected", "Protected");
            return RedirectToAction("Index");
        }
    }
}
