using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DessertsOrder : CommonEntities,IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public short HalfQuantity { get; set; }
        public short HalfPrice { get; set; }
        public short FullQuantity { get; set; }
        public short FullPrice { get; set; }
        [ForeignKey(nameof(CustomerOrder))]
        public Guid CustomerOrderId { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
    }
}
