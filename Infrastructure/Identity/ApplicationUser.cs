using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public UserType UserType { get;set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
