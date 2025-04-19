using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var userId = await _identityService.RegisterAsync(dto.Email, dto.Password,dto.FirstName,dto.LastName);
            return Ok(new { UserId = userId });
        }

        [HttpPost("login")]
        public async Task<ActionResult<LogInResponse>> Login([FromBody] LoginDto dto)
        {
            //_logger.LogInformation("This is an info log from the controller!");  //its here for testing purpose only 

            var response = await _identityService.LoginAsync(dto.Email, dto.Password);
            return Ok(response);
        }
    }
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
