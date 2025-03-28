using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerOrder 
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string TableNumber { get; set; }

    }
}
