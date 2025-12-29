using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Data;
using Repositories.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 1. DbContext Ekleme
            services.AddDbContext<DataApplicationContext>(options =>
                options.UseSqlite(connectionString));

            // 2. Identity Ekleme (Bu kalmalı, veritabanı ile ilgili)
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<DataApplicationContext>()
            .AddDefaultTokenProviders();

            // --- SİLİNEN KISIM BAŞLANGIÇ ---
            // BURADAKİ AddAuthentication ve AddJwtBearer KODLARINI SİLDİM.
            // ÇÜNKÜ ZATEN ServiceExtensions.cs İÇİNDE VARLAR.
            // --- SİLİNEN KISIM BİTİŞ ---

            services.AddCors(options =>
            {
                options.AddPolicy("ReactJSCors", policy =>
                {
                    policy.WithOrigins(
                            "https://localhost:3000",
                            "https://localhost:11405"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddHostedService<DataSeedHostedService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMasterProfileRepository, MasterProfileRepository>();
            return services;
        }
    }
}