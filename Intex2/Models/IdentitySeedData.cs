using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Intex2.Models {
    public class IdentitySeedData {

        public static void CreateAdminAccount(IServiceProvider serviceProvider,
                IConfiguration configuration) {
            CreateAdminAccountAsync(serviceProvider, configuration).Wait();
        }

        public static async Task CreateAdminAccountAsync(IServiceProvider
                serviceProvider, IConfiguration configuration) {

            serviceProvider = serviceProvider.CreateScope().ServiceProvider;

            UserManager<IdentityUser> userManager =
                serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = configuration["Data:AdminUser:Name"] ?? "admin";
            string email
                = configuration["Data:AdminUser:Email"] ?? "admin@example.com";
            string password = configuration["Data:AdminUser:Password"] ?? "secret";
            string role = configuration["Data:AdminUser:Role"] ?? "Admins";

            if (await userManager.FindByNameAsync(username) == null) {
                if (await roleManager.FindByNameAsync(role) == null) {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityUser user = new IdentityUser {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            username = configuration["Data:AdminUser:Name"] ?? "Logan";
            email = configuration["Data:AdminUser:Email"] ?? "Logan@Hardy.com";
            password = configuration["Data:AdminUser:Password"] ?? "secret";
            role = configuration["Data:AdminUser:Role"] ?? "Admins";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityUser user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            username = configuration["Data:AdminUser:Name"] ?? "Logan";
            email = configuration["Data:AdminUser:Email"] ?? "Logan@Hardy.com";
            password = configuration["Data:AdminUser:Password"] ?? "secret";
            role = configuration["Data:AdminUser:Role"] ?? "Admins";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityUser user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            username = configuration["Data:AdminUser:Name"] ?? "Jeff";
            email = configuration["Data:AdminUser:Email"] ?? "Jeff@Havell.com";
            password = configuration["Data:AdminUser:Password"] ?? "secret";
            role = configuration["Data:AdminUser:Role"] ?? "Admins";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityUser user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            username = configuration["Data:AdminUser:Name"] ?? "Quinn";
            email = configuration["Data:AdminUser:Email"] ?? "Quinn@Christensen.com";
            password = configuration["Data:AdminUser:Password"] ?? "secret";
            role = configuration["Data:AdminUser:Role"] ?? "Admins";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityUser user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            username = configuration["Data:AdminUser:Name"] ?? "Parker";
            email = configuration["Data:AdminUser:Email"] ?? "Parker@Mecham.com";
            password = configuration["Data:AdminUser:Password"] ?? "secret";
            role = configuration["Data:AdminUser:Role"] ?? "Admins";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityUser user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }


        }
    }
}
