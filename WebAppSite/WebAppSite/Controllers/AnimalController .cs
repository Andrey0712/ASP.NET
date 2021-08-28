using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAppSite.Domain;
using WebAppSite.Domain.Entities.Catalog;
using WebAppSite.Models;

namespace WebAppSite.Controllers
{
    public class AnimalController : Controller
    {
        private readonly AppEFContext _context;
        private readonly IMapper _mapper;
        private IHostEnvironment _host;
        public AnimalController(AppEFContext context,IMapper mapper, IHostEnvironment host)
        {
            _context = context;
            _mapper = mapper;
            _host = host;

            //GenerationAnimals();
        }
        private void GenerationAnimals()
            {
                int n = 10;
            var endDate = DateTime.Now;
            var startDate = new DateTime(endDate.Year - 2, endDate.Month, endDate.Day);
            var faker = new Faker<Animal>("uk").RuleFor(x => x.Name, f => f.Person.FullName)
                .RuleFor(x => x.DateBirth, f => f.Date.Between(startDate, endDate))
            .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
            .RuleFor(x=>x.Prise,f=>Decimal.Parse(f.Commerce.Price(100M,500M)))
            .RuleFor(x=>x.DateCreate,DateTime.Now);
            for (int i = 0; i < n; i++)
            {
                var animal = faker.Generate();
                _context.Animals.Add(animal);
                _context.SaveChanges();
            }

            }
        public IActionResult Index(SearchHomeIndexModel search, int page = 1, int nextItems=5)
        {
            HomeIndexModel model = new HomeIndexModel();

            int showItems = nextItems;//к-во записей на 1 стр
            var query = _context.Animals.AsQueryable();
            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));//поис по имени
            }
            

            //кількість записів, які ми знайшли загально
            int countItems = query.Count();
            var pageCount = (int)Math.Ceiling(countItems / (double)showItems);//к-во стр, округленых к большему числу
            if (pageCount == 0) pageCount = 1;

            int skipItems = (page - 1) * showItems;//ского записи показывать в зависимости от стр

            query = query.Skip(skipItems).Take(showItems);//сколько пропустить и сколько взять

            model.Animals = query
                .Select(x => _mapper.Map<AnimalViewModel>(x))
                .ToList();
            model.Search = search;
            model.Page = page;//номер поточноъ стр
            model.AddNextItems = nextItems;
            model.PageCount = pageCount;//к-во стр

            return View(model);
            //List<AnimalViewModel> model =


            //    //    new List<AnimalViewModel>();
            //    //model.Add(new AnimalViewModel
            //    //{
            //    //    Id=1,
            //    //    Birthday=DateTime.Now,
            //    //    Name="Енот-паласкун",
            //    //    Image="1.jpg"
            //    //});
            //    //model.Add(new AnimalViewModel
            //    //{
            //    //    Id = 2,
            //    //    Birthday = DateTime.Now.AddDays(-5),
            //    //    Name = "Кот-рыжий",
            //    //    Image = "2.jpg"
            //    //});


            //    _context.Animals.Select(x => new AnimalViewModel
            //    {
            //        Id = x.Id,
            //        Birthday = x.DateBirth,
            //        Image = x.Image,
            //        Name = x.Name
            //    }).ToList();

            //return View(model);
        }

        #region Animal Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnimalCreateViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);
            string fileName = "";
            if (model.Image != null)
            {

                //var filePath = Path.GetTempFileName();
                var ext = Path.GetExtension(model.Image.FileName);//разширение

                fileName = Path.GetRandomFileName() + ext;
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

                var filePath = Path.Combine(dir, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            DateTime dt = DateTime.Parse(model.BirthDay, new CultureInfo("uk-UA"));
            Animal animal = new Animal
            {
                Name = model.Name,
                DateBirth = dt,
                //DateBirth = model.BirthDay,
                Image = fileName,
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
AnimalCreateViewModel animal = new AnimalCreateViewModel();
            if (edit.Image != null)
            {
                
                var nameEditImage = Path.GetFileName(edit.Image);
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

                var nameEditImagePath = Path.Combine(dir, nameEditImage);
                using (var stream = System.IO.File.OpenRead($"{nameEditImagePath}"))
                {
                    var newImage = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

                    
                        animal.Name = edit.Name;
                    animal.Price = edit.Prise;
                    animal.BirthDay = edit.DateBirth.ToString();
                    animal.Image = newImage;
                   
                }
            }
            return View(animal);
        }

        [HttpPost]



        public async Task<IActionResult> Edit(AnimalCreateViewModel model, long id)
        {
            if (!ModelState.IsValid)
                return View(model);

            {

                var edit = _context.Animals.FirstOrDefault(x => x.Id == id);//редактируем полученный обьект



                edit.Name = model.Name;
                edit.DateBirth = DateTime.Parse(model.BirthDay, new CultureInfo("uk-UA"));
                //edit.Image = model.Image;

                string fileName = " ";


                if (model.Image != null)
                {
                    var imageForDell = Path.Combine(_host.ContentRootPath, "images", edit.Image);

                    var ext = Path.GetExtension(model.Image.FileName);//разширение

                    fileName = Path.GetRandomFileName() + ext;
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

                    var filePath = Path.Combine(dir, fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    if (System.IO.File.Exists(imageForDell))
                    {
                        System.IO.File.Delete(imageForDell);
                    }

                }


                edit.Image = fileName;
                edit.Prise = model.Price;
                

            }_context.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Animal Delete
        //[HttpGet]

        //public IActionResult Delete(long id)
        //{

        //    var del = _context.Animals.Find(id);
        //    if (del != null)
        //    {
        //        return View(new AnimalViewModel()
        //        {
        //            Id = del.Id,
        //            Name = del.Name,
        //            Birthday = del.DateBirth,
        //            Image = del.Image
        //        });
        //    }
        //    return NotFound();
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult Del(long id)
        //{

        //    var del = _context.Animals.Find(id);
        //    if (del != null)
        //    {
        //        _context.Animals.Remove(del);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //public IActionResult Delete(long id)
        //{
        //    Thread.Sleep(1000);
        //    var item = _context.Animals.SingleOrDefault(x => x.Id == id);
        //    if (item != null)
        //    {
        //        //_context.Remove(item);
        //        _context.Animals.Remove(item);
        //        _context.SaveChanges();
        //    }
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult Delete(long id)
        {
            Thread.Sleep(1000);
               var item = _context.Animals.SingleOrDefault(x => x.Id == id);
               if (item != null)
            {
                var imageForDell = Path.Combine(_host.ContentRootPath, "images", item.Image);

                if (System.IO.File.Exists(imageForDell))
                {
                    System.IO.File.Delete(imageForDell);
                }

                _context.Animals.Remove(item);
                      _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        #endregion
    }
}
