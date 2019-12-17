using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;

namespace WebStore.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(WebStoreContext webStoreContext)
        {
            webStoreContext.Database.EnsureCreated();

            if (webStoreContext.Products.Any())
                return;

            InitCategories(webStoreContext);
            InitBrands(webStoreContext);
            InitProducts(webStoreContext);
        }

        private static void InitProducts(WebStoreContext webStoreContext)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product12.jpg",
                    Order = 0,
                    CategoryId = 2,
                    BrandId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product11.jpg",
                    Order = 1,
                    CategoryId = 2,
                    BrandId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product10.jpg",
                    Order = 2,
                    CategoryId = 2,
                    BrandId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product9.jpg",
                    Order = 3,
                    CategoryId = 2,
                    BrandId = 1,
                    IsNew = true
                },
                new Product
                {
                    Id = 5,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product8.jpg",
                    Order = 4,
                    CategoryId = 2,
                    BrandId = 2,
                    IsSale = true
                },
                new Product
                {
                    Id = 6,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product7.jpg",
                    Order = 5,
                    CategoryId = 2,
                    BrandId = 2
                },
                new Product
                {
                    Id = 7,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product6.jpg",
                    Order = 6,
                    CategoryId = 2,
                    BrandId = 2
                },
                new Product
                {
                    Id = 8,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product5.jpg",
                    Order = 7,
                    CategoryId = 25,
                    BrandId = 2
                },
                new Product
                {
                    Id = 9,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product4.jpg",
                    Order = 8,
                    CategoryId = 25,
                    BrandId = 2
                },
                new Product
                {
                    Id = 10,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product3.jpg",
                    Order = 9,
                    CategoryId = 25,
                    BrandId = 3
                },
                new Product
                {
                    Id = 11,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product2.jpg",
                    Order = 10,
                    CategoryId = 25,
                    BrandId = 3
                },
                new Product
                {
                    Id = 12,
                    Name = "Easy Polo Black Edition",
                    Price = 1025,
                    ImageUrl = "product1.jpg",
                    Order = 11,
                    CategoryId = 25,
                    BrandId = 3
                }
            };

            using (var transaction = webStoreContext.Database.BeginTransaction())
            {
                foreach (var product in products)
                    webStoreContext.Products.Add(product);

                webStoreContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON");
                webStoreContext.SaveChanges();
                webStoreContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] OFF");
                transaction.Commit();
            }
        }

        private static void InitBrands(WebStoreContext webStoreContext)
        {
            var brands = new List<Brand>
            {
                new Brand
                {
                    Id = 1,
                    Name = "Acne",
                    Order = 0
                },
                new Brand
                {
                    Id = 2,
                    Name = "Grüne Erde",
                    Order = 1
                },
                new Brand
                {
                    Id = 3,
                    Name = "Albiro",
                    Order = 2
                },
                new Brand
                {
                    Id = 4,
                    Name = "Ronhill",
                    Order = 3
                },
                new Brand
                {
                    Id = 5,
                    Name = "Oddmolly",
                    Order = 4
                },
                new Brand
                {
                    Id = 6,
                    Name = "Boudestijn",
                    Order = 5
                },
                new Brand
                {
                    Id = 7,
                    Name = "Rösch creative culture",
                    Order = 6
                }
            };

            using (var transaction = webStoreContext.Database.BeginTransaction())
            {
                foreach (var brand in brands)
                    webStoreContext.Brands.Add(brand);

                webStoreContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] ON");
                webStoreContext.SaveChanges();
                webStoreContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                transaction.Commit();
            }
        }

        private static void InitCategories(WebStoreContext webStoreContext)
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Sportswear",
                    Order = 0,
                    ParentId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Nike",
                    Order = 0,
                    ParentId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = "Under Armour",
                    Order = 1,
                    ParentId = 1
                },
                new Category
                {
                    Id = 4,
                    Name = "Adidas",
                    Order = 2,
                    ParentId = 1
                },
                new Category
                {
                    Id = 5,
                    Name = "Puma",
                    Order = 3,
                    ParentId = 1
                },
                new Category
                {
                    Id = 6,
                    Name = "ASICS",
                    Order = 4,
                    ParentId = 1
                },
                new Category
                {
                    Id = 7,
                    Name = "Mens",
                    Order = 1,
                    ParentId = null
                },
                new Category
                {
                    Id = 8,
                    Name = "Fendi",
                    Order = 0,
                    ParentId = 7
                },
                new Category
                {
                    Id = 9,
                    Name = "Guess",
                    Order = 1,
                    ParentId = 7
                },
                new Category
                {
                    Id = 10,
                    Name = "Valentino",
                    Order = 2,
                    ParentId = 7
                },
                new Category
                {
                    Id = 11,
                    Name = "Dior",
                    Order = 3,
                    ParentId = 7
                },
                new Category
                {
                    Id = 12,
                    Name = "Versace",
                    Order = 4,
                    ParentId = 7
                },
                new Category
                {
                    Id = 13,
                    Name = "Armani",
                    Order = 5,
                    ParentId = 7
                },
                new Category
                {
                    Id = 14,
                    Name = "Prada",
                    Order = 6,
                    ParentId = 7
                },
                new Category
                {
                    Id = 15,
                    Name = "Dolce and Gabbana",
                    Order = 7,
                    ParentId = 7
                },
                new Category
                {
                    Id = 16,
                    Name = "Chanel",
                    Order = 8,
                    ParentId = 7
                },
                new Category
                {
                    Id = 17,
                    Name = "Gucci",
                    Order = 8,
                    ParentId = 7
                },
                new Category
                {
                    Id = 18,
                    Name = "Womens",
                    Order = 2,
                    ParentId = null
                },
                new Category
                {
                    Id = 19,
                    Name = "Fendi",
                    Order = 0,
                    ParentId = 18
                },
                new Category
                {
                    Id = 20,
                    Name = "Guess",
                    Order = 1,
                    ParentId = 18
                },
                new Category
                {
                    Id = 21,
                    Name = "Valentino",
                    Order = 2,
                    ParentId = 18
                },
                new Category
                {
                    Id = 22,
                    Name = "Dior",
                    Order = 3,
                    ParentId = 18
                },
                new Category
                {
                    Id = 23,
                    Name = "Versace",
                    Order = 4,
                    ParentId = 18
                },
                new Category
                {
                    Id = 24,
                    Name = "Kids",
                    Order = 3,
                    ParentId = null
                },
                new Category
                {
                    Id = 25,
                    Name = "Fashion",
                    Order = 4,
                    ParentId = null
                },
                new Category
                {
                    Id = 26,
                    Name = "Households",
                    Order = 5,
                    ParentId = null
                },
                new Category
                {
                    Id = 27,
                    Name = "Interiors",
                    Order = 6,
                    ParentId = null
                },
                new Category
                {
                    Id = 28,
                    Name = "Clothing",
                    Order = 7,
                    ParentId = null
                },
                new Category
                {
                    Id = 29,
                    Name = "Bags",
                    Order = 8,
                    ParentId = null
                },
                new Category
                {
                    Id = 30,
                    Name = "Shoes",
                    Order = 9,
                    ParentId = null
                }
            };

            using (var transaction = webStoreContext.Database.BeginTransaction())
            {
                foreach (var category in categories)
                    webStoreContext.Categories.Add(category);

                webStoreContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Categories] ON");
                webStoreContext.SaveChanges();
                webStoreContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Categories] OFF");
                transaction.Commit();
            }
        }

        public static void InitializeUsers(IServiceProvider services)
        {
            var roleManager = services.GetService<RoleManager<IdentityRole>>();
            EnsureRole(roleManager, "User");
            EnsureRole(roleManager, "Administrator");

            EnsureRoleToUser(services, "Admin", "Administrator", "admin123Admin@");
        }

        private static void EnsureRoleToUser(IServiceProvider services, 
            string userName, string roleName, string pass)
        {
            var userManager = services.GetService<UserManager<User>>();
            var users = services.GetService<IUserStore<User>>();

            if (users.FindByNameAsync(userName, CancellationToken.None).Result != null)
                return;

            var adminUser = new User
            {
                UserName = userName,
                Email = $"{userName}@domain.com"
            };

            if (userManager.CreateAsync(adminUser, pass).Result.Succeeded)
                userManager.AddToRoleAsync(adminUser, roleName).Wait();
        }

        private static void EnsureRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!roleManager.RoleExistsAsync(roleName).Result)
                roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
        }
    }
}
