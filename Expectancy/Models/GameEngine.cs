using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expectancy.Models
{
    public class GameEngine
    {
        public GameEngine()
        {
            CurrentDecision = new Decision();
            CurrentDecision.Id = 0;
        }

        public Decision CurrentDecision { get; set; }
    }
}
