using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Repositories
{
    public static class IdentitySeedData
    {
        private const string _adminUser = "Admin";
        private const string _adminPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app) {
            //служба для управления пользователями
            var userManager = app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(_adminUser);
            if (user == null) {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, _adminPassword);
            }
        }
    }
}
