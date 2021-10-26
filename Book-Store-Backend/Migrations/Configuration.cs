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
                    CategoryName = "Suspense and Thriller",
                    Description = "While they often encompass the same elements as mystery books, the suspense and thriller genre sees the hero attempt to stop and defeat the villain to save their own life rather than uncover a specific crime",
                    Position = 1,
                    Image = "/Content/Images/Suspense and Thriller.jpeg",
                },
                new Category()
                {
                    CategoryId = 2,
                    CategoryName = "Action and Adventure",
                    Description = "Action and adventure books constantly have you on the edge of your seat with excitement, as your fave main character repeatedly finds themselves in high stakes situations.",
                    Position = 2,
                    Image = "/Content/Images/Action and Adventure.jpg",
                },
                new Category()
                {
                    CategoryId = 3,
                    CategoryName = "Detective and Mystery",
                    Description = "The plot always revolves around a crime of sorts that must be solved—or foiled—by the protagonists.",
                    Position = 3,
                    Image = "/Content/Images/Detective and Mystery.jpg",
                },
                new Category()
                {
                    CategoryId = 4,
                    CategoryName = "Horror",
                    Description = "Meant to cause discomfort and fear for both the character and readers, horror writers often make use of supernatural and paranormal elements in morbid stories that are sometimes a little too realistic.",
                    Position = 4,
                    Image = "/Content/Images/horror.jpg",
                },
                new Category()
                {
                    CategoryId = 5,
                    CategoryName = "Comedy",
                    Description = "The stories in comic books and graphic novels are presented to the reader through engaging, sequential narrative art (illustrations and typography) that's either presented in a specific design or the traditional panel layout you find in comics",
                    Position = 5,
                    Image = "/Content/Images/Comedy.jpg",
                },
                new Category()
                {
                    CategoryId = 6,
                    CategoryName = "Romance",
                    Description = "The genre that makes your heart all warm and fuzzy focuses on the love story of the main protagonists.This world of fiction is extremely wide-reaching in and of itself, as it has a variety of sub-genres including: contemporary romance, historical, paranormal, and the steamier erotica",
                    Position = 6,
                    Image = "/Content/Images/Romance.jpg",
                }
            );
        }

        static float NextFloat(Random random)
        {
            return (float)(random.NextDouble()*5000)+100;
        }

        void CreateBooks(Book_Store_Backend.Models.ApplicationDbContext context)
        {
            Random random = new Random();
            context.Books.AddOrUpdate(
                new Book()
                {
                    BookId = 1,
                    Title = "Gone Girl",
                    Position = 1,
                    CategoryId = 1,
                    Price = NextFloat(random),
                    Description = "NAMED ONE OF THE MOST INFLUENTIAL BOOKS OF THE DECADE BY CNN NAMED ONE OF TIME’S TEN BEST FICTION BOOKS OF THE DECADE AND ONE OF ENTERTAINMENT WEEKLY’S BEST BOOKS OF THE DECADE",
                },
                new Book()
                {
                    BookId = 2,
                    Title = "And Then There Were None",
                    Position = 1,
                    CategoryId = 2,
                    Price = NextFloat(random),
                    Description = " Amongst all of Agata Christie works, none has a story quite as impeccably crafted as And Then There Were None, which explains why it is the best selling mystery book of all time. ",
                },
                new Book()
                {
                    BookId = 3,
                    Title = "The Silence of the Lambs",
                    Position = 1,
                    CategoryId = 3,
                    Price = NextFloat(random),
                    Description = "In this iconic suspense novel, FBI agent Clarice Stirling investigates a serial killer, “Buffalo Bill,” who preys on young women, and who potentially is linked to psychiatrist and cannibalistic murderer Hannibal Lecter.",
                },
                new Book()
                {
                    BookId = 4,
                    Title = "The Final Girl Support Group",
                    Position = 1,
                    CategoryId = 4,
                    Price = NextFloat(random),
                    Description = "Grady Hendrix’s book is part love letter to the slasher movie, and part deconstruction of the whole thing, as the survivors of a number of horrific slaughters (inspired, of course, by some iconic movies) get together to form a support group — except, of course, death isn’t quite done with all of them just yet.",
                },
                new Book()
                {
                    BookId = 5,
                    Title = "Based on a True Story",
                    Position = 1,
                    CategoryId = 5,
                    Price = NextFloat(random),
                    Description = "Comedians and comedy nerds love Norm, but why? It might be because he sarcastically and boldly ignores the rules, like regular joke rhythms or not being polite — he once told actress on Late Night that her terrible movie was terrible",
                },
                new Book()
                {
                    BookId = 6,
                    Title = "Honey Girl",
                    Position = 1,
                    CategoryId = 6,
                    Price = NextFloat(random),
                    Description = "Grace Porter goes on a girls’ trip to Vegas to celebrate her hard-earned PhD in astronomy. And while she’s a straitlaced gal who doesn’t get wild, this trip is an exception.",
                },
                new Book()
                {
                    BookId = 7,
                    Title = "The 19th Christmas",
                    Position = 2,
                    CategoryId = 2,
                    Price = NextFloat(random),
                    Description = "As the holidays approach, Detective Lindsay Boxer and her friends in the Women's Murder Club have much to celebrate. Crime is down. The medical examiner's office is quiet. Even the courts are showing some Christmas spirit. And the news cycle is so slow that journalist Cindy Thomas is on assignment to tell a story about the true meaning of the season for San Francisco. Then a fearsome criminal known only as 'Loman' seizes control of the headlines. Solving crimes never happens on schedule, but as this criminal mastermind unleashes credible threats by the hour, the month of December is upended for the Women's Murder Club. Avoiding tragedy is the only holiday miracle they seek. ",
                },
                
                new Book()
                {
                    BookId = 8,
                    Title = "Origin",
                    Position = 2,
                    CategoryId = 2,
                    Price = NextFloat(random),
                    Description = "The global bestseller - Origin is the latest Robert Langdon novel from the author of The Da Vinci Code.",
                },
                new Book()
                {
                    BookId = 9,
                    Title = "The Big Sleep",
                    Position = 2,
                    CategoryId = 3,
                    Price = NextFloat(random),
                    Description = "Raymond Chandler’s idea of mystery strays from conventions — for him it’s less about the intricate plot and more about the atmosphere and characters",
                },
                new Book()
                {
                    BookId = 10,
                    Title = "Horrid",
                    Position = 2,
                    CategoryId = 4,
                    Price = NextFloat(random),
                    Description = "Prepare for some understated, all-too-successful creepery.",
                },
                new Book()
                {
                    BookId = 11,
                    Title = "Born Standing Up",
                    Position = 2,
                    CategoryId = 5,
                    Price = NextFloat(random),
                    Description = "Including all entertainment memoirs, not just comedy people, is Steve Martin’s, and not just because of Martin’s unsurprisingly beautiful prose.",
                },
                new Book()
                {
                    BookId = 12,
                    Title = "Accidentally Engaged",
                    Position = 2,
                    CategoryId = 6,
                    Price = NextFloat(random),
                    Description = "Reena Manji’s parents are constantly meddling with her life and have been trying to find her a good potential Muslim husband. ",
                },
                new Book()
                {
                    BookId = 13,
                    Title = "The Guardians",
                    Position = 3,
                    CategoryId = 1,
                    Price = NextFloat(random),
                    Description = "Embrace yourself for the best novel in town",
                },
                new Book()
                {
                    BookId = 14,
                    Title = "Black Sun",
                    Position = 3,
                    CategoryId = 2,
                    Price = NextFloat(random),
                    Description = "'One of the best thrillers of recent years . . . a tour-de-force. It drips with authenticity from every page . . . a page-turning, thumping good read.' DAVID YOUNG, bestselling author of Stasi Child",
                },
                new Book()
                {
                    BookId = 15,
                    Title = "The Postman Always Rings Twice",
                    Position = 3,
                    CategoryId = 3,
                    Price = NextFloat(random),
                    Description = "The Postman Always Rings Twice is often lauded the most important crime book of the 20th century, and it's not hard to see why.",
                },
                new Book()
                {
                    BookId = 16,
                    Title = "Bossypants",
                    Position = 3,
                    CategoryId = 4,
                    Price = NextFloat(random),
                    Description = "ou can’t be Tina Fey, but you can get close to understanding what it is like to be Tina Fey, whose rise seems both hard and easy, probably because she is Tina Fey and equipped with the life, managerial, and comedic skill to do so and make it look easy (or at least make hard work look fun).",
                },
                new Book()
                {
                    BookId = 17,
                    Title = "The Soulmate Equation",
                    Position = 3,
                    CategoryId = 5,
                    Price = NextFloat(random),
                    Description = "Single mom Jess Davis is a data and statistics wizard, but she’s always been skeptical about putting herself out there again with dating.",
                },
                //new Book()
                //{
                //    BookId = 18,
                //    Title = "",
                //    Position = 3,
                //    CategoryId = 6,
                //    Price = NextFloat(random),
                //    Description = "",
                //},
                new Book()
                {
                    BookId = 19,
                    Title = "The Thirty-Nine Steps",
                    Position = 4,
                    CategoryId = 1,
                    Price = NextFloat(random),
                    Description = "Discover the original and best adventure story ever told",
                },
                new Book()
                {
                    BookId = 20,
                    Title = "The Da Vinci Code",
                    Position = 4,
                    CategoryId = 2,
                    Price = NextFloat(random),
                    Description = "Harvard professor Robert Langdon receives an urgent late-night phone call while on business in Paris: the elderly curator of the Louvre has been brutally murdered inside the museum. Alongside the body, police have found a series of baffling codes.",
                },
                new Book()
                {
                    BookId = 21,
                    Title = "Woman in White",
                    Position = 4,
                    CategoryId = 3,
                    Price = NextFloat(random),
                    Description = "This Wilkie Collins’s late Victorian novel is among the earliest psychological thrillers ever written.",
                },
                new Book()
                {
                    BookId = 22,
                    Title = "Tender is the Flesh",
                    Position = 4,
                    CategoryId = 4,
                    Price = NextFloat(random),
                    Description = "Meat, as Morrissey once memorably warbled, is murder. The connection between the two has rarely been as clear, or as disturbing, as in Bazterrica’s near-future tale where, after a mysterious virus makes animal flesh poisonous to people, the human race makes a shocking choice — and turns to cannibalism.",
                },
                new Book()
                {
                    BookId = 23,
                    Title = "Daddy’s Boy",
                    Position = 4,
                    CategoryId = 5,
                    Price = NextFloat(random),
                    Description = "Elliott, the “Guy Under the Stairs” on Late Night with David Letterman, the thirtysomething paperboy on Get a Life, and the cabin boy in Cabin Boy, has written a few fake nonfiction titles, such as Into Hot Air, about his disastrous, not-real ascent of Mount Everest",
                },
                new Book()
                {
                    BookId = 24,
                    Title = "The Ex Talk",
                    Position = 4,
                    CategoryId = 6,
                    Price = NextFloat(random),
                    Description = "Shay Goldstein loves her job as a producer at her Seattle public radio station. She has been working there for 10 years and can’t imagine working anywhere else.",
                },
                new Book()
                {
                    BookId = 25,
                    Title = "I Am Watching You",
                    Position = 5,
                    CategoryId = 1,
                    Price = NextFloat(random),
                    Description = "Why so many thrillers insist on taking place on trains, I’ll never know, but this book brings it in an entirely new way",
                },
                new Book()
                {
                    BookId = 26,
                    Title = "The Martian",
                    Position = 5,
                    CategoryId = 2,
                    Price = NextFloat(random),
                    Description = "A survival story for the 21st century and the international bestseller behind the major film from Ridley Scott, starring Matt Damon and Jessica Chastain.",
                },
                new Book()
                {
                    BookId = 27,
                    Title = "Anatomy of a Murder",
                    Position = 5,
                    CategoryId = 3,
                    Price = NextFloat(random),
                    Description = "Before there was How To Get Away With Murder and Suits, lawyer-related entertainment came in the form of criminal cases. Anatomy of a Murder, written by a Supreme Court Justice under the pseudonym Robert Traver, is such a classic.",
                },
                new Book()
                {
                    BookId = 28,
                    Title = "The Only Good Indians",
                    Position = 5,
                    CategoryId = 4,
                    Price = NextFloat(random),
                    Description = "A welcome entry into the “Something You Did Years Ago Is Back To Haunt You” hall of fame, Jones’ acclaimed 2020 novel sees four members of Blackfeet Nation having to deal with the aftermath of a hunting trip-gone-wrong a decade earlier.",
                },
                new Book()
                {
                    BookId = 29,
                    Title = "Forever, Erma",
                    Position = 5,
                    CategoryId = 5,
                    Price = NextFloat(random),
                    Description = "or most of us, our comedy education all started with the same things: Sesame Street, whatever comedy albums our parents had lying around, and whatever funny books were on our parents’ bookshelves.",
                },
                new Book()
                {
                    BookId = 30,
                    Title = "Seven Days in June",
                    Position = 5,
                    CategoryId = 6,
                    Price = NextFloat(random),
                    Description = "Single mom and best-selling erotica writer Eva Mercy unexpectedly meets award-winning novelist Shane Hall at a literary event.",
                },
                new Book()
                {
                    BookId = 31,
                    Title = "The Woman in the Window",
                    Position = 6,
                    CategoryId = 1,
                    Price = NextFloat(random),
                    Description = "This book was recently made into a movie starring Amy Adams, so you’ll def want to read it ASAP so you can be one of those “the book was better than the movie” people.",
                },
                new Book()
                {
                    BookId = 32,
                    Title = "How Sleep the Brave",
                    Position = 6,
                    CategoryId = 2,
                    Price = NextFloat(random),
                    Description = "Heroic and moving accounts of the R.A.F. in war time by the author of Fair Stood the Wind for France and The Darling Buds of May",
                },
                new Book()
                {
                    BookId = 33,
                    Title = "Tinker, Tailor, Soldier, Spy",
                    Position = 6,
                    CategoryId = 3,
                    Price = NextFloat(random),
                    Description = "Packed with interesting codenames and stressful covert actions, Tinker Tailor Soldier Spy is about an ex-spy, George Smiley (codename Beggarman), who is pulled out of retirement, to his relief, to weed out a Soviet mole in the British Intelligence Service.",
                },
                new Book()
                {
                    BookId = 34,
                    Title = "Wonderland",
                    Position = 6,
                    CategoryId = 4,
                    Price = NextFloat(random),
                    Description = "Released to comparisons of “The Shining,” there’s also a “Midsommar” feeling to this story of a family that escapes the rat race by moving to the country, only for things to fall apart",
                },
                new Book()
                {
                    BookId = 35,
                    Title = "Girl Walks Into a Bar",
                    Position = 6,
                    CategoryId = 5,
                    Price = NextFloat(random),
                    Description = "Girl Walks Into a Bar deals a lot with something that arts and entertainment books don’t touch on much — disappointment and the abrupt end of glory.",
                },
                new Book()
                {
                    BookId = 36,
                    Title = "People We Meet on Vacation",
                    Position = 6,
                    CategoryId = 6,
                    Price = NextFloat(random),
                    Description = "Alex and Poppy are completely different people, but somehow after sharing a care home years ago in college, they became the best of friends",
                }
            );
        }
    }
}
