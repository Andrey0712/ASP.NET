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

        #region Animal Create
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
        #endregion
        #region Animal Edit
        [HttpGet]
        
        public IActionResult Edit(long id)
        {
            var edit = _context.Animals.FirstOrDefault(x => x.Id == id);//вытягиваем с БД обект и заполняем форму его данными
            
            return View(new AnimalCreateViewModel()
            {
               
                Name = edit.Name,
                Price = edit.Prise,
                BirthDay = edit.DateBirth.ToString(),
                Image = edit.Image
            });


        }

        [HttpPost]
        
        public IActionResult Edit(AnimalCreateViewModel model,long id)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            {
                var edit = _context.Animals.FirstOrDefault(x => x.Id == id);//редактируем полученный обьект
                edit.Name = model.Name;
                edit.DateBirth = DateTime.Parse(model.BirthDay, new CultureInfo("uk-UA"));
                edit.Image = model.Image;
                edit.Prise = model.Price;
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Animal Delete
        [HttpGet]

        public IActionResult Del(long id)
        {
            var del = _context.Animals.FirstOrDefault(x => x.Id == id);
            
                return View(new AnimalCreateViewModel()
                {
                    Name = del.Name,
                    Price = del.Prise,
                    BirthDay = del.DateBirth.ToString(),
                    Image = del.Image
                });
           
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var del = _context.Animals.FirstOrDefault(x => x.Id == id);
           
                _context.Animals.Remove(del);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
        }
       




        #endregion
    }
}
