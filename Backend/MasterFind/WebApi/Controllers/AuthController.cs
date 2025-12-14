using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Auth.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (!result.Success)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(new { Token = result.Token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (!result.Success)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(new { Token = result.Token });
        }
    }
}
