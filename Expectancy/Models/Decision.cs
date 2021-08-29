using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expectancy.Models
{
    public class Decision
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Option> Options { get; set; }
    }
}
