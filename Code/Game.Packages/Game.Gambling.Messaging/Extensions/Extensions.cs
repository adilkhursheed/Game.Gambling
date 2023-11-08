
using Game.Gambling.Messaging.Interfaces;
using Game.Gambling.Messaging.Publishers;
using Game.Gambling.Messaging.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Game.Gambling.Messaging.Extension
{
    public static class Extensions
    {
        public static IServiceCollection AddMessagingDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(conf =>
            {
                conf.SetKebabCaseEndpointNameFormatter();

                //// By default, sagas are in-memory, but should be changed to a durable
                //// saga repository.
                //conf.SetInMemorySagaRepositoryProvider();

                conf.AddConsumers(Assembly.GetEntryAssembly());


                ////If any messaging Queue is not available then InMemory can also be used
                //conf.UsingInMemory((context, cfg) =>
                //{
                //    cfg.ConfigureEndpoints(context);
                //});

                var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                // Uncomment below if you have Rabbit up and running
                conf.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMQSettings.Host, "/", h =>
                    {
                        h.Username(rabbitMQSettings.UserName);
                        h.Password(rabbitMQSettings.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            })
            .AddScoped<IMessagePublisher,MessagePublisher>();
            return services;
        }
    }
}