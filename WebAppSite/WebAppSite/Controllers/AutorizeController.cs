using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAppSite.Controllers
{
    [Authorize]//зашита доступа
    public class AutorizeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]//могут попасть не авторизованіе юзері
        public IActionResult Login(string returnURL)
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]//могут попасть не авторизованіе юзері
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim("Demo","Value")
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPricipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPricipal);
            return Redirect(model.ReturnURL);
        }

        public IActionResult LogOff()
        {
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Index");
        }

    }

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ReturnURL { get; set; }
    }
}
