using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebAppSite.Domain;
using WebAppSite.Domain.Entities.Catalog;
using WebAppSite.Models;

namespace WebAppSite.Controllers
{
    public class AnimalController : Controller
    {
        private readonly AppEFContext _context;
        public AnimalController(AppEFContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<AnimalViewModel> model =


                //    new List<AnimalViewModel>();
                //model.Add(new AnimalViewModel
                //{
                //    Id=1,
                //    Birthday=DateTime.Now,
                //    Name="Енот-паласкун",
                //    Image="1.jpg"
                //});
                //model.Add(new AnimalViewModel
                //{
                //    Id = 2,
                //    Birthday = DateTime.Now.AddDays(-5),
                //    Name = "Кот-рыжий",
                //    Image = "2.jpg"
                //});

                
                _context.Animals.Select(x => new AnimalViewModel
                {
                    Id = x.Id,
                    Birthday = x.DateBirth,
                    Image = x.Image,
                    Name = x.Name
                }).ToList();

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnimalCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            DateTime dt = DateTime.Parse(model.BirthDay, new CultureInfo("uk-UA"));
            Animal animal = new Animal
            {
                Name = model.Name,
                DateBirth = dt,
                //DateBirth = model.BirthDay,
                Image = model.Image,
                Prise = model.Price,
                DateCreate = DateTime.Now
            };
            _context.Animals.Add(animal);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
