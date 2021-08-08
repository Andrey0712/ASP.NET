using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSite.Domain.Entities.Catalog;

namespace WebAppSite.Models
{
    public class AnimalProfil:Profile

    {
        public AnimalProfil()
        {
            CreateMap<AnimalViewModel, Animal>()
                .ForMember(x=>x.DateBirth,opt=>opt.MapFrom(x=>x.Birthday))
                .ReverseMap();//мапить в обе стороны//CreateMap<Animal, AnimalViewModel>();
            
           
        }
    }
}
