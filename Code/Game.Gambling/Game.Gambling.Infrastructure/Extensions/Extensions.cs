using Game.Gambling.Application.Applications;
using Game.Gambling.Application.Interfaces;
using Game.Gambling.Application.Validators;
using Game.Gambling.Domain.Entities;
using Game.Gambling.Infrastructure.Context;
using Game.Gambling.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<GamblingDBConext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPlayerGamblingDetailRepository, PlayerGamblingDetailRepository>();
            return services;
        }
    }
}