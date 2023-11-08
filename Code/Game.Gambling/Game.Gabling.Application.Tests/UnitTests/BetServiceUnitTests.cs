using Game.Gambling.Application.Applications;
using Game.Gambling.Application.Interfaces;
using Game.Gambling.Application.Tests.Factory;
using Game.Gambling.Application.Validators;
using Game.Gambling.Domain;
using Game.Gambling.Domain.DTOs;
using Game.Gambling.Domain.Entities;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Game.Gambling.Application.Tests.UnitTests
{
    public class BetServiceUnitTests
    {
        private MockDependencies mockDependencies= new();
        public BetServiceUnitTests()
        {

        }
        private string userId = "2c4cf163-3f29-4eb5-a572-06e6f4da79dc";
        private BetService GetBetService(BetDto betDTO, int fakeRandomNumber, long accountBalance = 10000, bool isUpdateSuccessfull = true, bool validationResult = true, string validationError = "")
        {
            var userGamblingDetailMock = new UserGamblingDetail(It.IsAny<int>(),It.IsAny<string>(), accountBalance);

            var userGamblingRepositoryMock = mockDependencies.MockPlayerGamblingDetailRepository(accountBalance, isUpdateSuccessfull);

            var betValidatorMock = mockDependencies.MockBetValidator(betDTO,accountBalance, validationResult, validationError);

            var randomMock = mockDependencies.MockRandom(fakeRandomNumber);

            return new BetService(userGamblingRepositoryMock, betValidatorMock, randomMock);
        }

        [Fact]
        public async Task Bet_Won()
        {
            try
            {
                //Arrange
                var betDTO = new BetDto(100, 5);
                var fakeRandomNumber = 5;
                BetService betService = GetBetService(betDTO, fakeRandomNumber);

                //Act
                var actualResult = await betService.Bet(betDTO, userId);

                //Assert
                Assert.Equal(BetStatusEnum.won.ToString(), actualResult.Status);
                Assert.Equal($"+900", actualResult.Points);
                Assert.Equal(10900, actualResult.Account);

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        [Fact]
        public async Task Bet_Lost()
        {
            //Arrange
            var betDTO = new BetDto(100, 5);
            var fakeRandomNumber = 1;
            BetService betService = GetBetService(betDTO, fakeRandomNumber);

            //Act
            var actualResult = await betService.Bet(betDTO, userId);

            //Assert
            Assert.Equal(BetStatusEnum.lost.ToString(), actualResult.Status);
            Assert.Equal($"-100", actualResult.Points);
            Assert.Equal(9900, actualResult.Account);

        }

        /// <summary>
        /// In this test initial balance is 10000, the player lost 1000 points 10 times.
        /// On 11th attempt it will error out
        /// </summary>
        [Fact]
        public async Task Bet_Balance_Washout()
        {
            //Arrange
            var betDTO = new BetDto(1000, 5);
            var fakeRandomNumber = 1;
            long currentBalance = 10000;
            string ActualResult = "";
            try
            {
                var userGamblingDetailMock = new UserGamblingDetail(It.IsAny<int>(),It.IsAny<string>(), currentBalance);
                
                // With actual bet validator so it pases until balance becomes zero
                var betValidatorActual = new BetValidator();
                var randomMock = mockDependencies.MockRandom(fakeRandomNumber);

                for (int i = 1; i <= 11; i++)
                {
                    var userGamblingRepositoryMock = mockDependencies.MockPlayerGamblingDetailRepository(currentBalance);
                    var betService = new BetService(userGamblingRepositoryMock, betValidatorActual, randomMock);
                    
                    //Act
                    var actualResult = await betService.Bet(betDTO, userId);
                    currentBalance -= betDTO.Points;
                    
                    //Assert
                    Assert.Equal(BetStatusEnum.lost.ToString(), actualResult.Status);
                    Assert.Equal($"-{betDTO.Points}", actualResult.Points);
                    Assert.Equal(currentBalance, actualResult.Account);
                }
            }
            catch (ValidationException ex)
            {
                ActualResult = ex.Message;
            }
            catch (Exception ex)
            {
                ActualResult = ex.Message;
            }

            Assert.Equal(Constants.Error_InvalidBalance, ActualResult);
            ////Assert
            //Assert.Equal(BetStatusEnum.lost.ToString(), actualResult.Status);
            //Assert.Equal($"-100", actualResult.Points);
            //Assert.Equal(9900, actualResult.Account);

        }

        [Fact]
        // 
        public async Task Bet_NumberGreaterThanBalance_ValidationFailed()
        {
            //Arrange
            var betDTO = new BetDto(100, 5);
            var fakeRandomNumber = 5;
            BetService betService = GetBetService(betDTO, fakeRandomNumber, accountBalance: 90, validationResult: false, validationError: Constants.Error_NumberGreaterThanBalance);

            //Act
            var actualResult = string.Empty;
            try
            {
                await betService.Bet(betDTO, userId);
            }
            catch (ValidationException ex)
            {
                actualResult = ex.Message;
            }
            catch (Exception ex)
            {
                actualResult = ex.Message;
            }

            //Assert
            Assert.Equal(Constants.Error_NumberGreaterThanBalance, actualResult);

        }

        public async Task Bet_InvalidBalance_Failure()
        {
            //Arrange
            var betDTO = new BetDto(100, 5);
            var fakeRandomNumber = 5;
            BetService betService = GetBetService(betDTO, fakeRandomNumber, accountBalance: 0, validationResult: false, validationError: Constants.Error_InvalidBalance);

            //Act
            var ActualResult = string.Empty;
            try
            {
                await betService.Bet(betDTO, userId);
            }
            catch (ValidationException ex)
            {
                ActualResult = ex.Message;
            }
            catch (Exception ex)
            {
                ActualResult = ex.Message;
            }

            //Assert
            Assert.Equal(Constants.Error_InvalidBalance, ActualResult);

        }

        [Fact]
        public async Task Bet_AccountBalanceUpdate_Failure()
        {
            //Arrange
            var betDTO = new BetDto(100, 5);
            var fakeRandomNumber = 5;
            BetService betService = GetBetService(betDTO, fakeRandomNumber, isUpdateSuccessfull: false);

            //Act
            var ActualResult = string.Empty;
            try
            {
                await betService.Bet(betDTO, userId);
            }
            catch (Exception ex)
            {
                ActualResult = ex.Message;
            }

            //Assert
            Assert.Equal(Constants.Error_FailedToUpdateBalance, ActualResult);

        }

        [Fact]
        public async Task Bet_InvalidNumber_Failure()
        {
            //Arrange
            var betDTO = new BetDto(5,0);
            var fakeRandomNumber = 5;
            BetService betService = GetBetService(betDTO, fakeRandomNumber, validationResult: false, validationError: Constants.Error_InvalidNumber);

            //Act
            var ActualResult = string.Empty;
            try
            {
                await betService.Bet(betDTO, userId);
            }
            catch (ValidationException ex)
            {
                ActualResult = ex.Message;
            }

            //Assert
            Assert.Equal(Constants.Error_InvalidNumber, ActualResult);

        }
        [Fact]
        public async Task Bet_NumberOutOfRange_Failure()
        {
            //Arrange
            var betDTO = new BetDto(100, 11);
            var fakeRandomNumber = 5;
            BetService betService = GetBetService(betDTO, fakeRandomNumber, validationResult: false, validationError: Constants.Error_NumberOutOfRange);

            //Act
            var ActualResult = string.Empty;
            try
            {
                await betService.Bet(betDTO, userId);
            }
            catch (ValidationException ex)
            {
                ActualResult = ex.Message;
            }

            //Assert
            Assert.Equal(Constants.Error_NumberOutOfRange, ActualResult);

        }
    }
}