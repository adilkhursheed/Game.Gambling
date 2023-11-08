using Game.Security.Application.Interfaces;
using Game.Security.Domain;
using Game.Security.Domain.Contexts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Security.API.Consumers
{
    public class NewUserRewardConsumerSecurity : IConsumer<PlayerCreatedContext>
    {

        public NewUserRewardConsumerSecurity()
        {
        }
        public async Task Consume(ConsumeContext<PlayerCreatedContext> context)
        {
            if (string.IsNullOrEmpty(context.Message.UserId))
            {

            }
        }
    }
}
