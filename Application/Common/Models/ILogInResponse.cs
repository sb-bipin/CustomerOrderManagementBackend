using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public interface ILogInResponse
    {
        string UserId { get; }
        string Token { get; }
    }
}

