using Microsoft.AspNetCore.Mvc;
using Skolaris.Services;

namespace Skolaris.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthApiController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthApiController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _authService.Login(request.Email, request.Password);

            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                user.Id,
                user.Nom,
                user.Email,
                user.Role
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}