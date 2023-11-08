using Game.Gambling.Application.Applications;
using Game.Gambling.Application.Interfaces;
using Game.Gambling.Application.Validators;
using Game.Gambling.Domain;
using Game.Gambling.Domain.DTOs;
using Game.Gambling.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Application.Tests.Factory
{
    public class MockDependencies
    {
        string userId;
        public IPlayerGamblingDetailRepository MockPlayerGamblingDetailRepository(long accountBalance = 10000, bool isUpdateSuccessfull = true)
        {
            var userGamblingDetailMock = new UserGamblingDetail(It.IsAny<int>(),It.IsAny<string>(), accountBalance);

            var userGamblingRepositoryMock = new Mock<IPlayerGamblingDetailRepository>();
            userGamblingRepositoryMock.Setup(repo => repo.GetByUserId(It.IsAny<string>()))
                .ReturnsAsync(() => userGamblingDetailMock);
            userGamblingRepositoryMock.Setup(repo => repo.Update(userGamblingDetailMock))
                .ReturnsAsync(() => isUpdateSuccessfull);
            return userGamblingRepositoryMock.Object;
        }
        public IBetValidator MockBetValidator(BetDto betDTO, long accountBalance = 10000,  bool validationResult = true, string validationError = "")
        {
            var betValidator = new Mock<IBetValidator>();
            betValidator.Setup(repo => repo.Validate(accountBalance, betDTO.Number, betDTO.Points, out validationError))
                .Returns(() => validationResult);

            return betValidator.Object;
        }
        public Random MockRandom( int fakeRandomNumber)
        {
            var fakeRandom = new Mock<Random>();
            fakeRandom.Setup(repo => repo.Next(Constants.BetMinNumber, Constants.BetMaxNumber + 1)).Returns(() => fakeRandomNumber);

            return fakeRandom.Object;
        }
    }
}
