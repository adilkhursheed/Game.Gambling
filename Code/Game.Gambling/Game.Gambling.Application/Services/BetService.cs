using Game.Gambling.Application.Interfaces;
using Game.Gambling.Application.Validators;
using Game.Gambling.Domain;
using Game.Gambling.Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Game.Gambling.Application.Applications
{
    public class BetService : IBetService
    {
        private readonly IPlayerGamblingDetailRepository userGamblingDetailRepository;
        private readonly IBetValidator betApplicationValidator;
        private readonly Random random;
        public BetService(IPlayerGamblingDetailRepository userGamblingDetailRepository,
            IBetValidator betApplicationValidator, Random random)
        {
            this.userGamblingDetailRepository = userGamblingDetailRepository;
            this.betApplicationValidator = betApplicationValidator;
            this.random = random;
        }

        public async Task<BetResultDto> Bet(BetDto dto, string userId)
        {
            var userGamblingDetail = await this.userGamblingDetailRepository.GetByUserId(userId);
            
            if (!this.betApplicationValidator.Validate(
                userGamblingDetail?.AccountBalance ?? 0,
                dto?.Number ?? 0,
                dto?.Points ?? 0, out var error))
            {
                throw new ValidationException(error);
            }
            var res = random.Next(Constants.BetMinNumber, Constants.BetMaxNumber+1);
            BetResultDto resultDto;
            if (dto.Number == res)
            {
                var reward = dto.Points * Constants.RewardMultiple;
                userGamblingDetail.AccountBalance += reward;
                resultDto = new BetResultDto(userGamblingDetail.AccountBalance, BetStatusEnum.won.ToString(), $"+{reward}");
            }
            else
            {
                userGamblingDetail.AccountBalance -= dto.Points;
                resultDto = new BetResultDto(userGamblingDetail.AccountBalance, BetStatusEnum.lost.ToString(), $"-{dto.Points}");
            }

            var isUpdated= await this.userGamblingDetailRepository.Update(userGamblingDetail);

            if (isUpdated)
            {
                return resultDto;
            }
            else
            {
                throw new Exception(Constants.Error_FailedToUpdateBalance);
            }
        }
    }
}
