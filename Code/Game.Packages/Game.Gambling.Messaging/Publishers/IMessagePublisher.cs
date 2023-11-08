namespace Game.Gambling.Messaging.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishUserCreated(string userId);
    }
}