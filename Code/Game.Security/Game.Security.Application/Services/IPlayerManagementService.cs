using Game.Security.Domain.Entities;

namespace Game.Security.Application.Services
{
    public interface IPlayerManagementService
    {
        Task<bool> Create(PlayerDto player);
    }
}