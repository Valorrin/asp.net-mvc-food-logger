using FoodLogger.Data.Enum;
using FoodLogger.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Net;

namespace FoodLogger.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Foods.Any())
                {
                    context.Foods.AddRange(new List<Food>()
                    {
                        new Food()
                        {
                            Name = "Banana",
                            Grams = 100,
                            Calories = 89,
                            Protein = 1.1,
                            Carbs = 20.2,
                            Fat = 0.3,
                            FoodCategory = FoodCategory.Fruit
                        },
                        new Food()
                        {
                            Name = "Walnuts",
                            Grams = 100,
                            Calories = 654,
                            Protein = 15.2,
                            Carbs = 7,
                            Fat = 65.2,
                            FoodCategory = FoodCategory.Nuts
                        },
                        new Food()
                        {
                            Name = "Potato",
                            Grams = 100,
                            Calories = 58,
                            Protein = 2.6,
                            Carbs = 9.9,
                            Fat = 0.1,
                            FoodCategory = FoodCategory.Vegetable
                        },
                        new Food()
                        {
                            Name = "Egg",
                            Grams = 100,
                            Calories = 155,
                            Protein = 12.6,
                            Carbs = 1.1,
                            Fat = 10.6,
                            FoodCategory = FoodCategory.Egg
                        },
                        new Food()
                        {
                            Name = "Chicken Breast",
                            Grams = 100,
                            Calories = 120,
                            Protein = 22.5,
                            Carbs = 0,
                            Fat = 2.6,
                            FoodCategory = FoodCategory.Poultry
                        },

                    });
                    context.SaveChanges();
                }

            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "valorrin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        Name = "valorrin",
                        UserName = "valorrin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "123456");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        Name = "app-user",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "123456");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

    }
      
}
