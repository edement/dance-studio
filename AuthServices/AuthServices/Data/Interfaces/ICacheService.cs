using AuthServices.Models;

namespace AuthServices.Data.Interfaces
{
    public interface ICacheService
    {
        T? GetData<T>(string key);
        void SetData<T>(string key, T data, int expiresMinutes);
        void RemoveData(string key);
    }
}
