using Microsoft.Extensions.Caching.Memory;
using MVCWEB.Services.Abstract;

namespace MVCWEB.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(10);

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T? Get<T>(string key)
        {
            _cache.TryGetValue(key, out T? value);
            return value;
        }
        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? DefaultExpiration,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            _cache.Set(key, value, options);
        }
        public void Remove(string key) => _cache.Remove(key);
        public bool TryGet<T>(string key, out T? value)
        {
            return _cache.TryGetValue(key, out value);
        }
        public async Task<T> GetOrCreateAsync<T>( string key, Func<Task<T>> factory, TimeSpan? expiration = null)
        {
            if (_cache.TryGetValue(key, out T? cached) && cached != null)
                return cached;

            var result = await factory();

            Set(key, result, expiration);

            return result;
        }
    }
}
