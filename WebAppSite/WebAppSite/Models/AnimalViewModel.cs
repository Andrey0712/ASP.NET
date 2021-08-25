using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSite.Models
{
    public class AnimalViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
    }
    #region Animal Create
    public class AnimalCreateViewModel
    {
        [Display(Name = "Назва тварини")]//датаанатация
        public string Name { get; set; }
        [Display(Name = "Дата народження")]
        public string BirthDay { get; set; }
        [Display(Name = "Фото")]
        public IFormFile  Image { get; set; }
        [Display(Name = "Ціна тварини")]
        public decimal Price { get; set; }
    }

    public class AnimalCreateValidator : AbstractValidator<AnimalCreateViewModel>
    {
        public AnimalCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Поле не може бути порожнім");
            RuleFor(x => x.BirthDay).NotEmpty()
                .WithMessage("Поле не може бути порожнім");
            RuleFor(x => x.Price).NotEmpty()
                .WithMessage("Поле не може бути порожнім");
        }
    }
    #endregion

    #region Search Animal

    public class SearchHomeIndexModel
    {
        public string Name { get; set; }
    }

    public class HomeIndexModel//разширяющая модель
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public List<AnimalViewModel> Animals { get; set; }
        public SearchHomeIndexModel Search { get; set; }
    }

    #endregion
}
