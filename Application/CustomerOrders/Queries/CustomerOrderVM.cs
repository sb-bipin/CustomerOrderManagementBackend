using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerOrders.Queries
{
    public class CustomerOrderVM
    {
        public string CustomerName { get; set; }
        public string TableNumber { get; set; }
        public Guid OrderNumber { get; set; }
        public ICollection<SnacksOrder> SnacksOrder { get; set; }
        public ICollection<DrinksOrder> DrinksOrder { get; set; }
        public ICollection<DessertsOrder> DessertsOrder { get; set; }
    }
}
