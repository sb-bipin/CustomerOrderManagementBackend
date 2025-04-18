using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class SeedInitialData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var serviceProviderScoped = scope.ServiceProvider;
            var loggerFactory = serviceProviderScoped.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("SeedInitialData");

            const string adminRole = "Admin";
            const string adminEmail = "admin@example.com";
            const string adminPassword = "Admin@123";

            try
            {
                // 1. Create Role if not exists
                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(adminRole));
                    logger.LogInformation("Seeded Role: {Role}", adminRole);
                }

                // 2. Create Admin User if not exists
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                        logger.LogInformation("Seeded Admin User: {Email}", adminEmail);
                    }
                    else
                    {
                        var errorDescriptions = string.Join(", ", result.Errors.Select(e => e.Description));
                        logger.LogError("Failed to create admin user: {Errors}", errorDescriptions);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
