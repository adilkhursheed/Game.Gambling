using Game.Gambling.Domain.Entities;
using Game.Gambling.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Application.Interfaces
{
    /// <summary>
    /// User repository responsible for User related DB operations.
    /// Implements general operations from IRepository and can have any specific User related operation here.
    /// </summary>

    public interface IPlayerGamblingDetailRepository : IRepository<UserGamblingDetail>
    {
        Task<UserGamblingDetail> GetByUserId(string id);

    }
}
