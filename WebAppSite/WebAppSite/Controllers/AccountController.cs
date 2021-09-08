using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAppSite.Domain.Entities.Identity;
using WebAppSite.Models;

namespace WebAppSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private IHostEnvironment _host;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
                                IHostEnvironment host)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _host = host;
        }
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(AvtorizViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,
                        model.Password, false, false);//логиним юзера
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Дані вказано не коректно");//указіваем ошибку
            return View(model);
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            //строчка для файлу фото профіля.            
            string fileNameUser = string.Empty;

            //якщо фото обрано:
            if (model.Image != null)
            {
                //розширення
                var ext = Path.GetExtension(model.Image.FileName);
                //імя файла з розширенням.
                fileNameUser = Path.GetRandomFileName() + ext;
                //директорія,де знаходитиметься файл.
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                //повний шлях до фото.
                var filePath = Path.Combine(dir, fileNameUser);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            //якщо користувач вже існує,то виводимо попередження.
            if (user != null)
            {
                ModelState.AddModelError("Email", " Користувач з такими даними вже існує ");
            }
            //якщо введені валідні дані для реєстрації користувача:
            if (ModelState.IsValid)
            {
                user = new AppUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    ImageProfile = fileNameUser
                };
                //var role = new AppRole
                //{
                //    Name = "User"
                //};

                //створили користувача.
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //додали роль користувачу.
                    //await _userManager.AddToRoleAsync(user, "User");
                    //залогінили його.
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //і перепаравили на головну сторінку.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Помилка.");
                }
            }
            return View(model);



        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
