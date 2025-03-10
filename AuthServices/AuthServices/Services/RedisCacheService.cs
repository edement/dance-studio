﻿using AuthServices.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AuthServices.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public T? GetData<T>(string key)
        {
            var data = _cache.GetString(key);
            if(data == null) { return default;  }

            return JsonSerializer.Deserialize<T>(data)!;
        }

        public void SetData<T>(string key, T data, int expiresMinutes)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiresMinutes)
            };

            _cache.SetString(key, JsonSerializer.Serialize(data), options);
        }
        public void DelData(string key)
        {
            _cache.Remove(key);
        }
    }
}
