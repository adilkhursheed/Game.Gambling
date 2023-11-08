using Game.Gambling.Messaging.Interfaces;
using Game.Security.Application.Interfaces;
using Game.Security.Application.Services;
using Game.Security.Domain.Entities;
using Moq;

namespace Game.Security.Tests.Application
{
    public class PlayerManagementServiceUnitTests
    {
        [Fact]
        public async void Player_Create_Success()
        {
            // Arrange
            var playerDto = new PlayerDto("","","");
            var userRepositoryMock = new Mock<IPlayerRepository>();
            userRepositoryMock.Setup(repo => repo.Save(playerDto)).ReturnsAsync(() => { playerDto.Id = "123"; return true; });

            var messagePublisher = new Mock<IMessagePublisher>();
            messagePublisher.Setup(repo => repo.PublishUserCreated(It.IsAny<string>()));

            var playerManagement = new PlayerManagementService(userRepositoryMock.Object, messagePublisher.Object);


            //Act
            var actualResult = await playerManagement.Create(playerDto);


            //Assert
            Assert.True(actualResult);
        }
        [Fact]
        public async void Player_Create_Failure()
        {
            // Arrange
            var playerDto = new PlayerDto("","","");
            var userRepositoryMock = new Mock<IPlayerRepository>();
            userRepositoryMock.Setup(repo => repo.Save(playerDto)).ReturnsAsync(true);

            var messagePublisher = new Mock<IMessagePublisher>();
            messagePublisher.Setup(repo => repo.PublishUserCreated(It.IsAny<string>()));

            var playerManagement = new PlayerManagementService(userRepositoryMock.Object, messagePublisher.Object);


            //Act
            var actualResult = await playerManagement.Create(playerDto);


            //Assert
            Assert.False(actualResult);
        }
    }
}