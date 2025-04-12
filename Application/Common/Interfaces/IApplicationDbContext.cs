using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<CustomerOrder> CustomerOrders { get; set; }
        DbSet<SnacksOrder> SnacksOrders { get; set; }
        DbSet<DrinksOrder> DrinksOrders { get; set; }
        DbSet<DessertsOrder> DessertsOrders { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
