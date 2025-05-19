using DocManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthenticationController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        // For demo purposes only: registers and logins with fixed username/password
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserCredentials creds)
        {
            // You can extend with DB to store users.
            return Ok(new { message = "User registered (demo only)" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials creds)
        {
            if (creds.Username == "testuser" && creds.Password == "password")
            {
                var token = _jwtService.GenerateToken(creds.Username);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }

    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
