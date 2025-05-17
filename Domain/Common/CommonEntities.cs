using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class CommonEntities
    {
        public DateTime? AddedOn { get; set; }
        public Guid? AddedBy { get; set; }
        public DateTime? ChangedOn { get; set; }
        public Guid? ChangedBy { get; set; }
    }
}
