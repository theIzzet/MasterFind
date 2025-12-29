using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Auth;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services.Admin;
using Services.Master;

namespace Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));
            services.AddScoped<JwtService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = configuration["JWT:ValidAudience"],
                     ValidIssuer = configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),

                     // Claim Eşleştirmeleri (Senin kodunda vardı, doğru)
                     NameClaimType = System.Security.Claims.ClaimTypes.NameIdentifier,
                     RoleClaimType = System.Security.Claims.ClaimTypes.Role,

                     // Saat farkı toleransı (Opsiyonel, genelde 5 dk'dır)
                     ClockSkew = TimeSpan.Zero
                 };

                 // --- HATA AYIKLAMA İÇİN EVENTS (BUNU EKLE) ---
                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine("------------------------------------------------");
                         Console.WriteLine("TOKEN HATASI: " + context.Exception.Message);
                         Console.WriteLine("------------------------------------------------");
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         Console.WriteLine("------------------------------------------------");
                         Console.WriteLine("TOKEN BAŞARILI: Kullanıcı doğrulandı.");
                         Console.WriteLine("------------------------------------------------");
                         return Task.CompletedTask;
                     },
                     OnChallenge = context =>
                     {
                         Console.WriteLine("------------------------------------------------");
                         Console.WriteLine("TOKEN REDDEDİLDİ (OnChallenge): " + context.Error + " - " + context.ErrorDescription);
                         Console.WriteLine("------------------------------------------------");
                         return Task.CompletedTask;
                     }
                 };
                 // ----------------------------------------------
             });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMasterProfileService, MasterProfileService>();
            services.AddScoped<IAdminService, AdminService>();

            return services;
        }
    }
}