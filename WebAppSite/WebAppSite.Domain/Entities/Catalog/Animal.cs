using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppSite.Domain.Entities.Catalog
{
    public class Animal: BaseEntity<long>
    {
        public string Name { get; set; }
        public decimal Prise { get; set; }
        public DateTime DateBirth { get; set; }
        public string Image { get; set; }
    }
}
