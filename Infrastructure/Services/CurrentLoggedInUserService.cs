// Infrastructure/Services/CurrentUserService.cs
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class CurrentLoggedInUserService : ICurrentLoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentLoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(userIdString, out var userId) ? userId : null;
            }
        }
    }
}
