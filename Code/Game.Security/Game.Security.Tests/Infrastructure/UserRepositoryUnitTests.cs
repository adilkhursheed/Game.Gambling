using Game.Gambling.Infrastructure.Persistence;
using Game.Gambling.Messaging.Interfaces;
using Game.Security.Application.Interfaces;
using Game.Security.Application.Services;
using Game.Security.Domain.Entities;
using Game.Security.Infrastructure.Entities;
using Game.Security.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Game.Security.Tests.Infrastructure
{

    public class PlayerManagementServiceUnitTests
    {
        [Fact]
        public async void Playermanagement_Create_Success()
        {
            // Arrange
            var playedto= new PlayerDto("","","");
            var mockUserManager = new Mock<UserManager<PlayerIdentity>>(
            new Mock<IUserStore<PlayerIdentity>>().Object, null, null, null, null, null, null, null, null);

            var user = playedto.Map();

            // Set up the UserManager method to return a success result
            mockUserManager.Setup(um => um.CreateAsync(It.IsNotNull<PlayerIdentity>(), playedto.Password))
                .ReturnsAsync(IdentityResult.Success);

            var userService = new UserRepository(mockUserManager.Object);

            // Act
            var result = await userService.Save(playedto);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async void Playermanagement_Create_Failure()
        {
            // Arrange
            var playedto= new PlayerDto("","","");
            var mockUserManager = new Mock<UserManager<PlayerIdentity>>(
            new Mock<IUserStore<PlayerIdentity>>().Object, null, null, null, null, null, null, null, null);

            // Set up the UserManager method to return a success result
            mockUserManager.Setup(um => um.CreateAsync(It.IsNotNull<PlayerIdentity>(), playedto.Password))
                .ReturnsAsync(new IdentityResult());

            var userService = new UserRepository(mockUserManager.Object);

            // Act
            var result = await userService.Save(playedto);

            // Assert
            Assert.False(result);
        }
    }
}