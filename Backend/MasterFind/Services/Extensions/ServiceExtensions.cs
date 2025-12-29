using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Admin;
using Services.Auth;
using Services.Master;
using System.Reflection;

namespace Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // SADECE JWT AYARLARINI AL (Service içinde kullanıldığı için)
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));

            // BURADAKİ AddAuthentication KODLARINI SİLDİK (RepositoryExtensions'a taşıdık)

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<JwtService>(); // JwtService burada kalabilir
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IMasterProfileService, MasterProfileService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}