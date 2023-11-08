using System.ComponentModel.DataAnnotations;

namespace Game.Security.API.DTOs
{
    public record CreatePlayerDto(
        [Required] string UserName,
        [Required] [EmailAddress] string Email,
        [Required] string Password);
}
