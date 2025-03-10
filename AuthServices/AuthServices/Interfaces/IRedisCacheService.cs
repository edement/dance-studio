namespace AuthServices.Interfaces
{
    public interface IRedisCacheService
    {
        T? GetData<T>(string key);
        void SetData<T>(string key, T data, int expiresMinutes);
        void DelData(string key);
    }
}
