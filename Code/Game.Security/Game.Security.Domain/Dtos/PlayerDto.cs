
namespace Game.Security.Domain.Entities
{
    public class PlayerDto
    {
        public PlayerDto(string userName, string email)
        {
            this.UserName = userName;
            this.Email = email;
        }
        public PlayerDto(string userName, string email, string Password)
        {
            this.UserName = userName;
            this.Email = email;
            this.Password = Password;
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}