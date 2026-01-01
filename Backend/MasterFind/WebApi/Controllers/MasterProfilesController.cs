using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Services.Master;
using Services.Master;
using Services.Master.Dto;
using Services.Master.Dtos;
using System.Security.Claims;
using System.Threading.Tasks;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterProfilesController : ControllerBase
    {
        private readonly IMasterProfileService _masterProfileService;

        public MasterProfilesController(IMasterProfileService masterProfileService)
        {
            _masterProfileService = masterProfileService;
        }
       
      
        [HttpPost]
        [Authorize(Roles = "User")] 
        public async Task<IActionResult> CreateProfile([FromForm] CreateMasterProfileDto profileDto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var result = await _masterProfileService.CreateProfileAsync(profileDto, currentUserId);
            if (!result.Success)
            {
                return BadRequest(result.Errors); // Servisten gelen hataları döner.
            }

            return CreatedAtAction(nameof(GetMyProfile), new { }, result);
        }

        [HttpGet("me")]
        [Authorize] 
        public async Task<IActionResult> GetMyProfile()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized(); // Token geçerli ama ID yoksa yetkisiz.
            }

            var result = await _masterProfileService.GetProfileByAppUserIdAsync(currentUserId);
            if (result == null)
            {
                return NotFound("Bu kullanıcıya ait bir usta profili bulunamadı.");
            }
            return Ok(result);
        }

   
        [HttpGet("{masterProfileId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfileByMasterProfileId(int masterProfileId)
        {
            var profile = await _masterProfileService.GetProfileByMasterProfileIdAsync(masterProfileId);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateMasterProfileDto profileDto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            var result = await _masterProfileService.UpdateProfileAsync(profileDto, currentUserId);
            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }
            return NoContent();
        }


        [HttpPost("portfolio")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreatePortfolioItem([FromForm] CreatePortfolioItemDto dto)
        {
        
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _masterProfileService.AddPortfolioItemAsync(userId, dto);
            if (!result.Success)
                return BadRequest(new { Errors = result.Errors });

            return CreatedAtAction(nameof(GetPortfolioItem), new { id = result.Data.Id }, result.Data);
        }
      
        
        
        [HttpDelete("portfolio/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeletePortfolioItem(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _masterProfileService.DeletePortfolioItemAsync(id, userId);
            if (!result.Success)
                return BadRequest(new { Errors = result.Errors });

            return NoContent();
        }

        [HttpGet("/portfolio/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPortfolioItem(int id)
        {
            var item = await _masterProfileService.GetPortfolioItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("{masterProfileId:int}/portfolio")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPortfolioForMaster(int masterProfileId)
        {
            var portfolioItems = await _masterProfileService.GetPortfolioItemsByMasterProfileIdAsync(masterProfileId);
            return Ok(portfolioItems);
        }

    }
}
