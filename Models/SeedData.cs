using System;
using System.Linq;
using LibApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                if (!context.MembershipTypes.Any())
                    SeedMembershipTypes(context);
                else
                    Console.WriteLine("Database already seeded with Customers.");

                if (!context.Genre.Any())
                    SeedGenres(context);
                else
                    Console.WriteLine("Database already seeded with Genres.");

                if (!context.Books.Any())
                    SeedBooks(context);
                else
                    Console.WriteLine("Database already seeded with Books.");

                if (!context.Roles.Any())
                    SeedRoles(context);
                else
                    Console.WriteLine("Database already seeded with roles.");

                if (!context.Customers.Any())
                    SeedCustomers(context, userManager);
                else
                    Console.WriteLine("Database already seeded with Books.");
            }
        }

        private static void SeedMembershipTypes(ApplicationDbContext context)
        {
            context.MembershipTypes.AddRange(
             new MembershipType
             {
                 Id = 1,
                 Name = "Pay as You Go",
                 SignUpFee = 0,
                 DurationInMonths = 0,
                 DiscountRate = 0
             },
             new MembershipType
             {
                 Id = 2,
                 Name = "Monthly",
                 SignUpFee = 30,
                 DurationInMonths = 1,
                 DiscountRate = 10
             },
             new MembershipType
             {
                 Id = 3,
                 Name = "Quaterly",
                 SignUpFee = 90,
                 DurationInMonths = 3,
                 DiscountRate = 15
             },
             new MembershipType
             {
                 Id = 4,
                 Name = "Yearly",
                 SignUpFee = 300,
                 DurationInMonths = 12,
                 DiscountRate = 20
             });

            context.SaveChanges();

        }
        private static void SeedBooks(ApplicationDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    GenreId = 1,
                    Name = "Miecz przeznaczenia",
                    AuthorName = "Andrzej Sapkowski",
                    ReleaseDate = DateTime.Parse("10/01/1992"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 92
                },
                new Book
                {
                    GenreId = 1,
                    Name = "Ostatnie życzenie",
                    AuthorName = "Andrzej Sapkowski",
                    ReleaseDate = DateTime.Parse("10/01/1993"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 93
                },
                new Book
                {
                    GenreId = 1,
                    Name = "Krew elfów",
                    AuthorName = "Andrzej Sapkowski",
                    ReleaseDate = DateTime.Parse("10/01/1994"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 94
                });

            context.SaveChanges();

        }

        private static void SeedGenres(ApplicationDbContext context)
        {
            context.Genre.AddRange(
                new Genre
                {
                    Id = 1,
                    Name = "Fantasy"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Romance"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Criminal"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Sci-Fi"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Horror"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Biography"
                }
            );

            context.SaveChanges();

        }

        private static void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddRange(
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "user"
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "StoreManager",
                        NormalizedName = "storemanager"
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Owner",
                        NormalizedName = "owner"
                    }
                );

            context.SaveChanges();

        }

        private static void SeedCustomers(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            var hasher = new PasswordHasher<Customer>();
            var customer1 = new Customer
            {
                Name = "Jakub Drzewo",
                Email = "jakub@drzewo.pl",
                NormalizedEmail = "jakub@drzewo.pl",
                UserName = "jakub@drzewo.pl",
                NormalizedUserName = "jakub@drzewo.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(customer1).Wait();
            userManager.AddToRoleAsync(customer1, "owner").Wait();

            var customer2 = new Customer
            {
                Name = "Ewa Jabłko",
                Email = "ewa@jablko.pl",
                NormalizedEmail = "ewa@jablko.pl",
                UserName = "ewa@jablko.pl",
                NormalizedUserName = "ewa@jablko.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(customer2).Wait();
            userManager.AddToRoleAsync(customer2, "user").Wait();

            var customer3 = new Customer
            {
                Name = "Tomek Zapałka",
                Email = "tomek@zapalka.pl",
                NormalizedEmail = "tomek@zapalka.pl",
                UserName = "tomek@zapalka.pl",
                NormalizedUserName = "tomek@zapalka.pl",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };

            userManager.CreateAsync(customer3).Wait();
            userManager.AddToRoleAsync(customer3, "storemanager").Wait();


            context.SaveChanges();

        }
    }
}