using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomerOrders.Command.AddCustomerOrder
{
    public class CreateCustomerOrderDto
    {
        public string CustomerName { get; set; }
        public string TableNumber { get; set; }
        public List<SnacksOrderDto> SnacksOrder { get; set; }
        public List<DrinksOrderDto> DrinksOrder { get; set; }
        public List<DessertsOrderDto> DessertsOrder { get; set; }
    }
    public class SnacksOrderDto
    {
        public string Name { get; set; }
        public short HalfQuantity { get; set; }
        public short HalfPrice { get; set; }
        public short FullQuantity { get; set; }
        public short FullPrice { get; set; }
    }

    public class DrinksOrderDto
    {
        public string Name { get; set; }
        public short HalfQuantity { get; set; }
        public short HalfPrice { get; set; }
        public short FullQuantity { get; set; }
        public short FullPrice { get; set; }
    }

    public class DessertsOrderDto
    {

        public string Name { get; set; }
        public short HalfQuantity { get; set; }
        public short HalfPrice { get; set; }
        public short FullQuantity { get; set; }
        public short FullPrice { get; set; }
    }
}
