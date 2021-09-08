using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppSite.Domain.Entities.Identity
{
    public class AppUser:IdentityUser<long>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public string ImageProfile { get; set; }
    }
}
