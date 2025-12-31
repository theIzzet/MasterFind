using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Auth.Dtos;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
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
            return BadRequest(new { Errors = result.Errors });

        HttpContext.Session.SetString("JWT", result.Token);

        return Ok(new { Message = "Login successful" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);

        if (!result.Success)
            return BadRequest(new { Errors = result.Errors });

        HttpContext.Session.SetString("JWT", result.Token);

        return Ok(new { Message = "Register successful" });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok(new { Message = "Logged out" });
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        return Ok(new
        {
            id = userId,
            roles = roles
        });
    }
}
