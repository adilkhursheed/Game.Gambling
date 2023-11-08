using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Domain
{
    public class Constants
    {
        public const int BetMinNumber=0;
        public const int BetMaxNumber=9;
        public const int RewardMultiple=9;
        public const int NewPlayerRewardPoints=10000;

        /// <summary>
        /// TODO, i18Next for translation
        /// </summary>
        public const string Error_InvalidBalance = "Can't Bet with zero balance";
        public const string Error_InvalidNumber = "Number should be greater than zero";
        public const string Error_NumberGreaterThanBalance = "Number should be less than account balance";
        public const string Error_NumberOutOfRange = "Number should be between {0} {1}";
        public const string Error_FailedToUpdateBalance="Failed to update Account Balance";
    }
}
