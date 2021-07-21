using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSite.Models;

namespace WebAppSite.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            List<AnimalViewModel> model = new List<AnimalViewModel>();
            model.Add(new AnimalViewModel
            {
                Id=1,
                Birthday=DateTime.Now,
                Name="Енот-паласкун",
                Image="1.jpg"
            });
            model.Add(new AnimalViewModel
            {
                Id = 2,
                Birthday = DateTime.Now.AddDays(-5),
                Name = "Кот-рыжий",
                Image = "2.jpg"
            });
            return View(model);
        }
    }
}
