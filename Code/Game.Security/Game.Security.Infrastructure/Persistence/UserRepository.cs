using Game.Security.Application.Interfaces;
using Game.Security.Domain.Entities;
using Game.Security.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Game.Security.Infrastructure.Extensions;

namespace Game.Gambling.Infrastructure.Persistence
{
    /// <summary>
    /// User repository responsible for User related DB operations.
    /// Inherits the regular operations from RepositoryBase and can have any specific User related operation here.
    /// </summary>
    public class UserRepository : IPlayerRepository
    {
        private readonly UserManager<PlayerIdentity> userManager;

        public UserRepository(UserManager<PlayerIdentity> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<PlayerDto> GetById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            return (await this.userManager.FindByIdAsync(userId))?.Map();
        }
        public async Task<PlayerDto> GetByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }
            return (await this.userManager.FindByNameAsync(userName))?.Map();
        }

        public async Task<bool> Save(PlayerDto playerDto)
        {
            if (playerDto == null)
            {
                return false;
            }
            var identitytosave= playerDto.Map();
            var identity= await this.userManager.CreateAsync(identitytosave, playerDto.Password);
            if (identity.Succeeded)
            {
                playerDto.Id = identitytosave.Id;
            }
            return identity.Succeeded;
        }

        public async Task<bool> Update(PlayerDto entity)
        {
            if (entity == null)
            {
                return false;
            }
            return (await this.userManager.UpdateAsync(entity.Map())).Succeeded;
        }
        public async Task<PlayerDto> CheckPassword(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            var user = await userManager.FindByNameAsync(userName);

            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                return user.Map(); ; // Return the user if valid
            }
            return null;
        }
    }
}
