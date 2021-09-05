using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Expectancy.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Expectancy.Helpers
{
    public class DataHelper
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string cachekey = "decisions";
        private readonly int LOOSE_END_ERROR = -1;

        public DataHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Decision Get(int id)
        {
            List<Decision> decisions;

            if (!_memoryCache.TryGetValue(cachekey, out decisions))
            {
                decisions = JsonConvert.DeserializeObject<List<Decision>>(File.ReadAllText("./Data/decisions.json"));

                _memoryCache.Set(
                    cachekey,
                    decisions,
                    new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1))
                );
            }

            var decision = decisions.FirstOrDefault(d => d.Id == id);
            return decision ?? decisions.FirstOrDefault(d => d.Id == LOOSE_END_ERROR);
        }
    }
}
