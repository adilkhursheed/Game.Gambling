using Game.Security.Application.Interfaces;
using Game.Security.Application.Services;
using Game.Gambling.Infrastructure.Persistence;
using Game.Security.Infrastructure.Data;
using Game.Security.Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Game.Security.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddSecurityInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<PlayerIdentity, IdentityRole>()
                .AddEntityFrameworkStores<AuthContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SignatureKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddDbContext<AuthContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                ); // Use your connection string

            services.AddScoped<IPlayerRepository, UserRepository>();
            services.AddScoped<IPlayerManagementService, PlayerManagementService>();
            return services;
        }
    }
}