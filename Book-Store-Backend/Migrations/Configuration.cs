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

            CreateUsers(context);
            CreateCategories(context);
            CreateBooks(context);
            context.SaveChanges();
        }
        void CreateUsers(Book_Store_Backend.Models.ApplicationDbContext context)
        {
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
        }

        void CreateCategories(Book_Store_Backend.Models.ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(
                new Category()
                {
                    CategoryId = 1,
                    CategoryName = "Thriller",
                    Description = "Just for the thrill",
                    Position = 1,
                },
                new Category()
                {
                    CategoryId = 2,
                    CategoryName = "Action",
                    Description = "Just for the thrill",
                    Position = 2,
                },
                new Category()
                {
                    CategoryId = 3,
                    CategoryName = "Mystery",
                    Description = "Just for the thrill",
                    Position = 3,
                },
                new Category()
                {
                    CategoryId = 4,
                    CategoryName = "Horror",
                    Description = "Just for the thrill",
                    Position = 4,
                },
                new Category()
                {
                    CategoryId = 5,
                    CategoryName = "Comedy",
                    Description = "Just for the thrill",
                    Position = 5,
                },
                new Category()
                {
                    CategoryId = 6,
                    CategoryName = "Romance",
                    Description = "Just for the thrill",
                    Position = 6,
                }
            );
        }

        void CreateBooks(Book_Store_Backend.Models.ApplicationDbContext context)
        {
            context.Books.AddOrUpdate(
                new Book()
                {
                    BookId = 1,
                    Title = "",
                    Position = 1,
                    CategoryId = 1,
                    Description = "",
                },
                new Book()
                {
                    BookId = 2,
                    Title = "",
                    Position = 1,
                    CategoryId = 2,
                    Description = "",
                },
                new Book()
                {
                    BookId = 3,
                    Title = "",
                    Position = 1,
                    CategoryId = 3,
                    Description = "",
                },
                new Book()
                {
                    BookId = 4,
                    Title = "",
                    Position = 1,
                    CategoryId = 4,
                    Description = "",
                },
                new Book()
                {
                    BookId = 5,
                    Title = "",
                    Position = 1,
                    CategoryId = 5,
                    Description = "",
                },
                new Book()
                {
                    BookId = 6,
                    Title = "",
                    Position = 1,
                    CategoryId = 6,
                    Description = "",
                }
            );
        }
    }
}
