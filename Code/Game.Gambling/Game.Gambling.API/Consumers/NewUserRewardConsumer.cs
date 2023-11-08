using Game.Gambling.Application.Interfaces;
using Game.Gambling.Domain;
using Game.Gambling.Messaging.Contexts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.API.Consumers
{
    public class NewUserRewardConsumer : IConsumer<PlayerCreatedContext>
    {
        private readonly IPlayerGamblingDetailRepository userGamblingDetailRepository;

        public NewUserRewardConsumer(IPlayerGamblingDetailRepository userGamblingDetailRepository)
        {
            this.userGamblingDetailRepository = userGamblingDetailRepository;
        }
        public async Task Consume(ConsumeContext<PlayerCreatedContext> context)
        {
            await this.userGamblingDetailRepository.Save(new Domain.Entities.UserGamblingDetail(context.Message.UserId, Constants.NewPlayerRewardPoints));
        }
    }
}
