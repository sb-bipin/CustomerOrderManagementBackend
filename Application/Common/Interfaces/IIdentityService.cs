using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> RegisterAsync(string email, string password,string firstName,string lastName);
        Task<ILogInResponse> LoginAsync(string email, string password);
        Task<bool> AddToRoleAsync(string userId, string role);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<List<IApplicationUser>> GetAllUsersAsync();
    }
}
