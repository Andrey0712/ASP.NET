using System;
using System.Collections.Generic;
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
}
