using Game.Security.Application.Interfaces;
using Game.Security.Application.Services;
using Game.Security.Domain.Entities;
using Game.Security.API.DTOs;
using Game.Security.Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Game.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerManagementController : ControllerBase
    {
        private readonly IPlayerRepository userRepository;
        private readonly IPlayerManagementService playerManagementApplication;

        public PlayerManagementController(IPlayerRepository userRepository, IPlayerManagementService playerManagementApplication)
        {
            this.userRepository = userRepository;
            this.playerManagementApplication = playerManagementApplication;
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<PlayerDto>> GetByUsername(string username)
        {
            var user = await this.userRepository.GetByUserName(username);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{userid}")]
        public async Task<ActionResult<PlayerDto>> GetById(string userid)
        {
            var user = await this.userRepository.GetById(userid);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePlayerDto playerDto)
        {
            var player= new PlayerDto(playerDto.UserName, playerDto.Email, playerDto.Password);
            if (await this.playerManagementApplication.Create(player))
            {
                return CreatedAtAction(nameof(this.GetById), routeValues: new { userid = player.Id },
                    new UserCreatedResultDto(playerDto.UserName, playerDto.Email));
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
