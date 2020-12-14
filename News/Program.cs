﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using News.Controllers.V1;
using News.Data;
using News.Domain;

namespace News
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<MyUser>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var adminRole = new IdentityRole("Admin");
                    await roleManager.CreateAsync(adminRole);
                }
                
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    var userRole = new IdentityRole("User");
                    await roleManager.CreateAsync(userRole);
                }                
                if (!await roleManager.RoleExistsAsync("Teatcher"))
                {
                    var userRole = new IdentityRole("Teatcher");
                    await roleManager.CreateAsync(userRole);
                }

                var adminExists = await userManager.GetUsersInRoleAsync("Admin");
                if (adminExists.Count <= 0)
                {
                    var newUserId = Guid.NewGuid();
                    var newUser = new MyUser
                    {
                        Id = newUserId.ToString(),
                        Email = "admin@example.com",
                        UserName = "admin"
                    };
                    await userManager.CreateAsync(newUser, "String1234.");
                    await userManager.AddToRoleAsync(newUser, "Admin");
                }
                
                var teatcherExists = await userManager.GetUsersInRoleAsync("Teatcher");
                if (adminExists.Count <= 0)
                {
                    var newUserId = Guid.NewGuid();
                    var newUser = new MyUser
                    {
                        Id = newUserId.ToString(),
                        Email = "teatcher@example.com",
                        UserName = "teatcher"
                    };
                    await userManager.CreateAsync(newUser, "String1234.");
                    await userManager.AddToRoleAsync(newUser, "Teatcher");
                }
            }
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
