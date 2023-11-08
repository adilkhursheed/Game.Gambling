using Game.Gambling.Domain.DTOs;

namespace Game.Gambling.Application.Applications
{
    public interface IBetService
    {
        Task<BetResultDto> Bet(BetDto dto,string userId);
    }
}