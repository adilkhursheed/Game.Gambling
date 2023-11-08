using Game.Gambling.Domain;
using Game.Gambling.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Application.Validators
{
    public class BetValidator : IBetValidator
    {
        public bool Validate(long accountBalance, long Number, long points, out string error)
        {
            error = string.Empty;
            if (accountBalance <= 0)
            {
                error = Constants.Error_InvalidBalance;
                return false;
            }
            if (Number <= 0)
            {
                error = Constants.Error_InvalidNumber;
                return false;
            }
            if (accountBalance < points)
            {
                error = Constants.Error_NumberGreaterThanBalance;
                return false;
            }
            if (Number < Constants.BetMinNumber || Number > Constants.BetMaxNumber)
            {
                error = string.Format(Constants.Error_NumberOutOfRange, Constants.BetMinNumber, Constants.BetMaxNumber);
                return false;
            }

            return true;
        }
    }
}
