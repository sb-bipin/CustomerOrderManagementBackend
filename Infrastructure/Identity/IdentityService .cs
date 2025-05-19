using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public IdentityService(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager,
                               ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<List<IApplicationUser>> GetAllUsersAsync()
        {
            var users = await _userManager.Users
                                .Select(user => new ApplicationUserDto
                                {
                                    Id = user.Id,
                                    Email = user.Email,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    UserType = user.UserType,
                                    CreatedBy = user.CreatedBy,
                                    CreatedOn = user.CreatedOn,
                                    UpdatedBy = user.UpdatedBy,
                                    UpdatedOn = user.UpdatedOn
                                })
                                .ToListAsync();


            if(users is null || !users.Any())
                return new List<IApplicationUser>();

            return users.Cast<IApplicationUser>().ToList();
        }
        public async Task<string> RegisterAsync(string email, string password,string firstName,string lastName)
        {
            var user = new ApplicationUser {
                UserName = email,
                NormalizedUserName = email.ToUpperInvariant(),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                FirstName = firstName,
                LastName = lastName,
                UserType = Domain.Enums.UserType.Customer,
                CreatedBy = new Guid(),
                CreatedOn = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            return user.Id;
        }

        public async Task<ILogInResponse> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("Username not found for login.");

            var result = await _signInManager.PasswordSignInAsync(user.UserName!, password, false, false);

            if (!result.Succeeded)
                throw new Exception("Invalid login attempt.");

            var token = _tokenService.GenerateJwtToken(user);

            return new LogInResponse
            {
                UserId = user.Id,
                Token = token
            };
        }

        public async Task<bool> AddToRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
