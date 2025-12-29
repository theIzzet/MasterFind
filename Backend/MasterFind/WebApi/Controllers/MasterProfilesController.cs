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
       
        // ui.Server/Controllers/MasterProfilesController.cs


        /// <summary>
        /// Giriş yapmış ve "Usta" rolüne sahip kullanıcı için yeni bir profil oluşturur.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "User")] // Sadece "Usta" rolündeki kullanıcılar erişebilir.
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

            // Profil oluşturulduktan sonra, oluşturulan profile yönlendirme yapmak best practice'tir.
            return CreatedAtAction(nameof(GetMyProfile), new { }, result);
        }
        /// <summary>
        /// Giriş yapmış olan kullanıcının kendi usta profilini getirir.
        /// </summary>
        [HttpGet("me")]
        [Authorize] // Sadece giriş yapmış kullanıcılar erişebilir.
        public async Task<IActionResult> GetMyProfile()
        {
            // Token'dan gelen claim'ler arasından kullanıcının ID'sini güvenli bir şekilde alıyoruz.
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

        /// <summary>
        /// Belirli bir ustanın herkese açık profilini getirir.
        /// </summary>
        [HttpGet("{masterProfileId:int}")] // masterProfileId'nin int tipinde olduğunu belirtiyoruz.
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


        /// <summary>
        /// Giriş yapmış ve "Usta" rolüne sahip kullanıcının kendi profilini günceller.
        /// </summary>
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
            return NoContent(); // Başarılı güncelleme sonrası 204 No Content dönmek yaygındır.
        }


        // --- YENİ PORTFOLYO OLUŞTURMA ---
        [HttpPost("portfolio")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreatePortfolioItem([FromForm] CreatePortfolioItemDto dto)
        {
            // Bu metodun imzası aynı kalır. ASP.NET Core, formdan gelen
            // aynı isimdeki ("Images") çoklu dosyaları ICollection<IFormFile> olarak
            // otomatik olarak bağlayacaktır.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _masterProfileService.AddPortfolioItemAsync(userId, dto);
            if (!result.Success)
                return BadRequest(new { Errors = result.Errors });

            return CreatedAtAction(nameof(GetPortfolioItem), new { id = result.Data.Id }, result.Data);
        }
      
        
        
        //// --- GÜNCELLENMİŞ PORTFOLYO GÜNCELLEME ENDPOINT'İ ---
        //[HttpPut("{id}")]
        //[Authorize(Roles = "User")]
        //public async Task<IActionResult> UpdatePortfolioItem(int id, [FromForm] UpdatePortfolioItemDto dto) // <-- [FromBody] -> [FromForm] olarak güncellendi!
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (string.IsNullOrEmpty(userId)) return Unauthorized();

        //    var result = await _masterProfileService.UpdatePortfolioItemAsync(id, userId, dto);
        //    if (!result.Success)
        //        return BadRequest(new { Errors = result.Errors });

        //    return NoContent();
        //}

        // --- PORTFOLYO SİLME ---
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

        // --- TEK BİR PORTFOLYO ÖĞESİNİ GETİRME (HERKESE AÇIK) ---
        [HttpGet("/portfolio/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPortfolioItem(int id)
        {
            var item = await _masterProfileService.GetPortfolioItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // --- BİR USTANIN TÜM PORTFOLYOSUNU GETİRME (HERKESE AÇIK) ---
        [HttpGet("{masterProfileId:int}/portfolio")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPortfolioForMaster(int masterProfileId)
        {
            var portfolioItems = await _masterProfileService.GetPortfolioItemsByMasterProfileIdAsync(masterProfileId);
            return Ok(portfolioItems);
        }

    }
}
