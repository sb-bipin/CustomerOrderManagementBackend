using Application.Common.Interfaces;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public interface IApplicationUser
    {
        public UserType UserType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }

        public new string? NormalizedUserName { get; set; }
        public new string? Email { get; set; }
        public new string? NormalizedEmail { get; set; }
        public new bool? EmailConfirmed { get; set; }
        public new string? SecurityStamp { get; set; }
        public new string? ConcurrencyStamp { get; set; }
        public new string? PhoneNumber { get; set; }
        public new bool? PhoneNumberConfirmed { get; set; }
        public new bool? TwoFactorEnabled { get; set; }
        public new DateTimeOffset? LockoutEnd { get; set; }
        public new bool? LockoutEnabled { get; set; }
        public new int? AccessFailedCount { get; set; }

    }
}
