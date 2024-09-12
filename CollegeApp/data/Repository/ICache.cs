namespace CollegeApp.data.Repository
{
    public interface ICache
    {

        Task<T> GetData<T>(string key);
        Task SetData<T>(string key, T value, TimeSpan expirationTime);
        Task RemoveData(string key);

    }
}
