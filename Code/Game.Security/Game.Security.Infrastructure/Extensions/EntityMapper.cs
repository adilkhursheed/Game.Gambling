using Game.Security.Domain.Entities;
using Game.Security.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Security.Infrastructure.Extensions
{
    public static class EntityMapper
    {
        public static PlayerDto Map(this PlayerIdentity player)
        {
            if (player == null)
            {
                return null;
            }
            return new PlayerDto(player.UserName, player.Email);
        }
        public static PlayerIdentity Map(this PlayerDto player)
        {
            if (player == null)
            {
                return null;
            }
            return new PlayerIdentity() {UserName=player.UserName, Email=player.Email };
        }
    }
}
