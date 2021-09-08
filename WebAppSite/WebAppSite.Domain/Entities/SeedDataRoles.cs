using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppSite.Domain.Entities.Identity;

namespace WebAppSite.Domain.Entities
{
    public static class SeedDataRoles
    {
        public static void Seeder(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var context = serviceScope.ServiceProvider.GetRequiredService<AppEFContext>();

            if (!roleManager.Roles.Any())
            {
                var role = new AppRole
                {
                    Name = "Admin"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "user@ani.mal",
                    UserName = "user@ani.mal",
                };
                var result = userManager.CreateAsync(user, "qwerty").Result;

                //додає користувача до іменованої ролі.
                result = userManager.AddToRoleAsync(user, "Admin").Result;
            }
        }
    }
}
