namespace Book_Store_Backend.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Book_Store_Backend.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    internal sealed class Configuration : DbMigrationsConfiguration<Book_Store_Backend.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Book_Store_Backend.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            string[] roles = new string[] { "Admin", "User" };
            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    context.Roles.Add(new IdentityRole(role));
                }
            }
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser
                {
                    Email = "xxxx@example.com",
                    UserName = "Admin",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = userManager.PasswordHasher.HashPassword("secret"),
                    LockoutEnabled = true,
                };
                userManager.CreateAsync(user).Wait();
                userManager.AddToRoleAsync(user.Id, "Admin").Wait();
            }

            context.SaveChanges();
        }
    }
}
