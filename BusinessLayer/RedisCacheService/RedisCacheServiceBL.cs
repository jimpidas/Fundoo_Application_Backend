using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RedisCacheService
{
    class RedisCacheServiceBL
    {
        private readonly IDistributedCache distributedCache;

        public RedisCacheServiceBL(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task RemoveNotesRedisCache(int UserID)
        {
            var cacheKey = UserID.ToString();
            await distributedCache.RemoveAsync(cacheKey);
           
        }
        public async Task AddNotesRedisCache(string cacheKey, object obj)
        {
            string serializedNotes = JsonConvert.SerializeObject(obj);
            var redisNoteCollection = Encoding.UTF8.GetBytes(serializedNotes);
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await distributedCache.SetAsync(cacheKey, redisNoteCollection, options);
           
        }
    }
}
