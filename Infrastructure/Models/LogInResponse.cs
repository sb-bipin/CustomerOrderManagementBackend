using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class LogInResponse : ILogInResponse
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}
