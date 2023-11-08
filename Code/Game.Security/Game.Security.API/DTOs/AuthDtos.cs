using System.ComponentModel.DataAnnotations;

namespace Game.Security.API.DTOs
{
    public record TokenRequestDto([Required]string UserName, [Required]string Password);
}
