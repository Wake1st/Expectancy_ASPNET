using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expectancy.Models
{
    public class GameEngine
    {
        public bool HasSave { get; set; }
        public int CurrentDecision { get; set; }
        public List<Option> Choices { get; set; }
    }
}
