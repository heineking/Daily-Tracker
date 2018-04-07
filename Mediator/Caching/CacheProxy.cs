using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mediator.Caching {
  // TODO: I wonder if this would be better off in a different project? Maybe the Infrastructure project?
  public class CacheProxy<T> : DispatchProxy {
    private IMemoryCache _cache;
    private MemoryCacheEntryOptions _entryOptions;
    private T _proxied;

    public void Decorate(T proxy) {
      _proxied = proxy;
    }

    public void SetCache(IMemoryCache cache) {
      _cache = cache;
    }

    public void SetCacheDirectives(uint cacheForSeconds, CacheType cacheType) {
      var timespan = TimeSpan.FromSeconds(cacheForSeconds);
      var cacheEntryOptions = new MemoryCacheEntryOptions();
      switch (cacheType) {
        case CacheType.Absolute:
          cacheEntryOptions.SetAbsoluteExpiration(timespan);
          break;
      }
      _entryOptions = cacheEntryOptions;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args) {
      var key = $"{targetMethod.DeclaringType.FullName}_{JsonConvert.SerializeObject(args)}";
      // look for the cache key
      if (!_cache.TryGetValue(key, out object cacheEntry)) {
        // key not in cache, so get the data
        cacheEntry = targetMethod.Invoke(_proxied, args);

        // save the data to the cache
        _cache.Set(key, cacheEntry, _entryOptions);
      }
      return cacheEntry;
    }
  }
}
