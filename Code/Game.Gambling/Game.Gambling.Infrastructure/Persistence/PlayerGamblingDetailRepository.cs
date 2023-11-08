using Game.Gambling.Application.Interfaces;
using Game.Gambling.Domain.Entities;
using Game.Gambling.Infrastructure.Context;
using Game.Gambling.Infrastructure.Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Infrastructure.Persistence
{
    /// <summary>
    /// UserGamblingDetailRepository is responsible for UserGamblingDetail DB operations
    /// </summary>
    public class PlayerGamblingDetailRepository : RespositoryBase<UserGamblingDetail>, IPlayerGamblingDetailRepository
    {
        #region Constructor
        public PlayerGamblingDetailRepository(GamblingDBConext gamblingDBContext) : base(gamblingDBContext)
        {

        }
        #endregion

        #region Public Methods
        public async Task<UserGamblingDetail> GetByUserId(string id)
        {
            return (await Get(x => x.UserId == id)).FirstOrDefault();
        }
        #endregion
    }
}
