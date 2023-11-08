using Game.Security.Domain.Entities;
using Game.Gambling.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Security.Application.Interfaces
{
    public interface IPlayerRepository
    {
        Task<PlayerDto> CheckPassword(string userName, string password);
        Task<PlayerDto> GetById(string userId);
        Task<PlayerDto> GetByUserName(string userName);
        Task<bool> Save(PlayerDto entity);
        Task<bool> Update(PlayerDto entity);
    }
}
