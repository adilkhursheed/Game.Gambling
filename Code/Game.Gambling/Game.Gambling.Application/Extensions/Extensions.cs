using Game.Gambling.Application.Applications;
using Game.Gambling.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Application.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddSingleton(typeof(Random));
            services.AddScoped<IBetValidator, BetValidator>();
            services.AddScoped<IBetService, BetService>();
            return services;
        }
    }
}