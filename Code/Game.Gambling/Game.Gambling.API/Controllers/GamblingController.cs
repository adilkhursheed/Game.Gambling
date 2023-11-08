using Game.Gambling.Application;
using Game.Gambling.Application.Applications;
using Game.Gambling.Application.Interfaces;
using Game.Gambling.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Game.Gambling.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamblingController : ControllerBase
    {
        private readonly IBetService betApplication;

        public GamblingController(IBetService betApplication)
        {
            this.betApplication = betApplication;
        }
        
        [HttpPost("Bet")]
        public async Task<ActionResult<BetResultDto>> Bet([FromBody] BetDto betDto)
        {
            try
            {
                var result= await this.betApplication.Bet(betDto, HttpContext.User.Claims.FirstOrDefault(x=> x.Type== ClaimTypes.NameIdentifier).Value);

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
