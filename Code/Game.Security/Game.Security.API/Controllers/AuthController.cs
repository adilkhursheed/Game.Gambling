using Game.Security.API.DTOs;
using Game.Security.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Game.Security.API.Controllers
{
    public class AuthController : Controller
    {
        #region Fields
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<PlayerIdentity> userManager;
        private readonly SymmetricSecurityKey _key;
        #endregion

        #region Constructor
        public AuthController(ILogger<AuthController> logger, UserManager<PlayerIdentity> userManager, IConfiguration configuration)
        {
            _logger = logger;
            this.userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SignatureKey"]));
        }
        #endregion

        #region API Endpoints
        [HttpPost]
        [Route("Token")]
        public async Task<IActionResult> Token([FromBody] TokenRequestDto tokenRequestDto)
        {
            // Validate user credentials and generate a JWT token
            var user = await ValidateUserAsync(tokenRequestDto.UserName, tokenRequestDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }
        #endregion

        #region Private Methods
        private async Task<PlayerIdentity> ValidateUserAsync(string userName, string password)
        {
            // Perform user validation using UserManager
            var user = await userManager.FindByNameAsync(userName);

            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                return user; // Return the user if valid
            }

            return null;
        }

        private string GenerateJwtToken(PlayerIdentity user)
        {
            var credentials = new SigningCredentials(this._key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
                // Add additional claims as needed
            };

            var token = new JwtSecurityToken(
                //"your-issuer",  // Replace with your issuer
                //"your-audience",  // Replace with your audience
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),  // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
