using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSite.Domain.Entities.Identity;
using WebAppSite.Models;

namespace WebAppSite.ViewComponents
{
   
        [Authorize]//может візівать только авторизированній юзер
        public class NavbarUserViewComponent : ViewComponent
        {
            private readonly UserManager<AppUser> _userManager;
        public NavbarUserViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);//получаем с базы
                UserNavbarInfoViewModel model = new UserNavbarInfoViewModel
                {
                    //FullName = "Василь Петрович",
                    //Image = "https://animalsglobe.ru/wp-content/uploads/2013/01/enot.jpg"

                    FullName = user.UserName,
                    //Image = user.ImageProfile
                };
            return View("_UserNavbarInfo", model);//вьюшка _UserNavbarInfo в которую запихнули model
        }
        }
   
}
