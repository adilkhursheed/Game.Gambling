using Game.Security.Application.Interfaces;
using Game.Security.Domain.Entities;
using Game.Gambling.Messaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Security.Application.Services
{
    public class PlayerManagementService : IPlayerManagementService
    {
        private readonly IPlayerRepository userRepository;
        private readonly IMessagePublisher messagePublisher;

        public PlayerManagementService(IPlayerRepository userRepository, IMessagePublisher messagePublisher)
        {
            this.userRepository = userRepository;
            this.messagePublisher = messagePublisher;
        }

        public async Task<bool> Create(PlayerDto player)
        {
            try
            {
                if (await userRepository.Save(player) && !string.IsNullOrEmpty(player.Id))
                {
                    await messagePublisher.PublishUserCreated(player.Id);
                    return true;
                }
            }
            catch
            {
                //maybe delete the created user?? if publish is failed
                throw;
            }
            return false;
        }
    }
}
