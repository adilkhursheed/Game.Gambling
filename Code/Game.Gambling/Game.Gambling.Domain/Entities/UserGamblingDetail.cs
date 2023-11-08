using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Domain.Entities
{
    public class UserGamblingDetail : BaseEntity
    {
        public UserGamblingDetail()
        {

        }
        public UserGamblingDetail(long Id, string userId, long accountBalance)
        {
            base.Id = Id;
            UserId = userId;
            AccountBalance = accountBalance;
        }
        public UserGamblingDetail(string userId, long accountBalance)
        {
            UserId = userId;
            AccountBalance = accountBalance;
        }
        public string UserId { get; set; }

        /// <summary>
        /// Account Score Balance
        /// </summary>
        public long AccountBalance { get; set; }

    }
}
