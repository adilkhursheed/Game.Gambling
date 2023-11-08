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
    /// User repository responsible for User related DB operations.
    /// Inherits the regular operations from RepositoryBase and can have any specific User related operation here.
    /// </summary>
    public class UserRepository : RespositoryBase<Player>, IUserRepository
    {
        public UserRepository(GamblingDBConext gamblingDBContext) : base(gamblingDBContext)
        {

        }
    }
}
