namespace Game.Gambling.Application.Validators
{
    public interface IBetValidator
    {
        bool Validate(long accountBalance, long Number, long points, out string error);
    }
}