using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Domain.DTOs
{
    public record BetDto(long Points, short Number);
    public record BetResultDto(long Account, string Status, string Points);
}
