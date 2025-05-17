using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerOrder : CommonEntities,IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string TableNumber { get; set; }
        public Guid OrderNumber { get; set; }
        public ICollection<SnacksOrder> SnacksOrder { get; set; }
        public ICollection<DrinksOrder> DrinksOrder { get; set; }
        public ICollection<DessertsOrder> DessertsOrder { get; set; }

    }
}
