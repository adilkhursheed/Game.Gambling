﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Security.Domain.Contexts
{
    public class PlayerCreatedContext
    {
        public PlayerCreatedContext(string userId)
        {
            this.UserId = userId;
        }
        public string UserId { get; set; }
    }
}
