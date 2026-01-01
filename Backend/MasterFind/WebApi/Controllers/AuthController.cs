using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Services.Auth;
using Services.Auth.Dtos;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("AuthPolicy")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    private void SetJwtCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,    // JS bu cookie'yi okuyamaz (XSS Koruması)
            Expires = DateTime.UtcNow.AddHours(1), // Token süresiyle aynı olsun
            Secure = true,      // HTTPS zorunlu (Localhost'ta çalışması için SSL kullanmalısın)
            SameSite = SameSiteMode.None, // React(5173) ve API(7054) farklı portta olduğu için 'None' şart
            // Production'da SameSiteMode.Lax veya Strict yapıp aynı domainde kullanmalısın.
            Path = "/", // Tüm uygulamada geçerli
            IsEssential = true // GDPR için gerekli
        };

        Response.Cookies.Append("jwt", token, cookieOptions);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);

        if (!result.Success)
            return BadRequest(new { Errors = result.Errors });

        //HttpContext.Session.SetString("JWT", result.Token);

        SetJwtCookie(result.Token);

        return Ok(new { Message = "Login successful" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);

        if (!result.Success)
            return BadRequest(new { Errors = result.Errors });

        //HttpContext.Session.SetString("JWT", result.Token);
        
        SetJwtCookie(result.Token);

        return Ok(new { Message = "Register successful" });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        //HttpContext.Session.Clear();

        Response.Cookies.Delete("jwt", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });
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
