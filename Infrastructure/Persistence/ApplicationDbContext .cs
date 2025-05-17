using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IApplicationDbContext
    {
        private readonly ICurrentLoggedInUserService _currentLoggedInUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentLoggedInUserService currentLoggedInUserService)
            : base(options)
        {
            _currentLoggedInUserService = currentLoggedInUserService;
        }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<SnacksOrder> SnacksOrders { get; set; }
        public DbSet<DrinksOrder> DrinksOrders { get; set; }
        public DbSet<DessertsOrder> DessertsOrders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var userId = _currentLoggedInUserService.UserId;

            foreach (var entry in ChangeTracker.Entries<CommonEntities>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.AddedOn = now;
                    entry.Entity.AddedBy = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ChangedOn = now;
                    entry.Entity.ChangedBy = userId;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
