﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expectancy.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            GameEngine = new GameEngine();
        }

        public bool HasGame { get; set; }
        public GameEngine GameEngine { get; set; }
    }
}
