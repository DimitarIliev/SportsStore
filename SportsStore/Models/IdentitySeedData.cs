using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder application)
        {
            using (var serviceScope = application.ApplicationServices.CreateScope())
            {
                UserManager<IdentityUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();

                IdentityUser user = await userManager.FindByIdAsync(adminUser);
                if (user == null)
                {
                    user = new IdentityUser("Admin");
                    await userManager.CreateAsync(user, adminPassword);
                }
            }
        }
    }
}
