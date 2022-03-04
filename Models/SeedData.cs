using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();
                if (context.MembershipTypes.Any() && context.Genre.Any() && context.Customers.Any() && context.Books.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }

                context.MembershipTypes.AddRange(
                new MembershipType
                {
                    Id = 1,
                    SignUpFee = 0,
                    DurationInMonths = 0,
                    DiscountRate = 0,
                    Name = "Pay as You GO"
                },
                new MembershipType
                {
                    Id = 2,
                    SignUpFee = 30,
                    DurationInMonths = 1,
                    DiscountRate = 10,
                    Name = "Monthly"
                },
                new MembershipType
                {
                    Id = 3,
                    SignUpFee = 90,
                    DurationInMonths = 3,
                    DiscountRate = 15,
                    Name = "Quaterly"
                },
                new MembershipType
                {
                    Id = 4,
                    SignUpFee = 300,
                    DurationInMonths = 12,
                    DiscountRate = 20,
                    Name = "Yearly"
                });

                context.Customers.AddRange(
                new Customer
                {
                    Name = "Steven Gerrard",
                    Birthdate = new DateTime(1980 , 5 , 30),
                    HasNewsletterSubscribed = true,
                    MembershipTypeId = 1
                },
                new Customer
                {
                    Name = "Frank Lampard",
                    Birthdate = new DateTime(1978, 6, 20),
                    HasNewsletterSubscribed = false,
                    MembershipTypeId = 2
                },
                new Customer
                {
                    Name = "Stefan Lipa",
                    Birthdate = new DateTime(1996, 9, 3),
                    HasNewsletterSubscribed = false,
                    MembershipTypeId = 2,
                });

                context.Books.AddRange(
                new Book
                {
                    ReleaseDate = new DateTime(1997, 6, 26),
                    AuthorName = "J.K.Rowling",
                    GenreId = 6,
                    Name = "Harry Potter i kamień filozoficzny",
                    NumberInStock = 100,
                },
                new Book
                {
                    ReleaseDate = new DateTime(1887, 2, 2),
                    AuthorName = "Carlo Collodi",
                    GenreId = 6,
                    Name = "Pinokio",
                    NumberInStock = 15,
                },
                new Book
                {
                    ReleaseDate = new DateTime(1906, 3, 12),
                    AuthorName = "Ferenc Molnár",
                    GenreId = 1,
                    Name = "Chłopcy z placu broni",
                    NumberInStock = 50,
                });
                context.SaveChanges();
            }
        }
    }
}