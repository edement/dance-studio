using AuthServices.Interfaces;
using Microsoft.Extensions.Options;

namespace AuthServices.Data
{
    public class TokenRepository(IRedisCacheService _cache) : ITokenRepository
    {
        public string? GetRefreshToken(string key)
        {
            return _cache.GetData<string>(key);
        }

        public void SaveRefreshToken(string refreshToken, string data)
        {
            _cache.SetData(refreshToken, data, 1);
        }
        public void DelRefreshToken(string key)
        {
            _cache.DelData(key);
        }
    }
}
