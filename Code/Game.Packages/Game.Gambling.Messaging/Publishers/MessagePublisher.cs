using Game.Gambling.Messaging.Contexts;
using Game.Gambling.Messaging.Interfaces;
using MassTransit;

namespace Game.Gambling.Messaging.Publishers
{
    public class MessagePublisher : IMessagePublisher
    {
        #region Private Fields
        private readonly IPublishEndpoint publishEndpoint;
        #endregion

        #region Constructor
        public MessagePublisher(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }
        #endregion

        #region Public Methods
        public Task PublishUserCreated(string userId)
        {
            return this.publishEndpoint.Publish<PlayerCreatedContext>(new PlayerCreatedContext(userId));
        }
        #endregion
    }
}
