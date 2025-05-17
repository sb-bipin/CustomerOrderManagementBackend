using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.UserSecrets;
using Application.Common.Interfaces;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: true)
                                    .AddUserSecrets<ApplicationDbContextFactory>()
                                    .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"A connection string named '{connectionString}' was not found in appsettings.json or secrets.json.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            var currentLoggedInUserService = new DesignTimeCurrentUserService();

            return new ApplicationDbContext(optionsBuilder.Options, currentLoggedInUserService);
        }
    }
}



public class DesignTimeCurrentUserService : ICurrentLoggedInUserService
{
    public Guid? UserId => Guid.Empty;
}
