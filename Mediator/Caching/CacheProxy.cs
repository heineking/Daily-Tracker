using Mediator.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mediator.Caching {
  public class CacheProxy<T> : DispatchProxy {
    private Cache _cache;
    private T _proxied;

    public void Decorate(T proxy) {
      _proxied = proxy;
    }

    public void SetCache(Cache cache) {
      _cache = cache;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args) {
      var key = $"{targetMethod.DeclaringType.FullName}_{JsonConvert.SerializeObject(args)}";
      if (_cache._dict.TryGetValue(key, out var cacheHit)) {
        return cacheHit;
      }

      var result = targetMethod.Invoke(_proxied, args);
      _cache._dict.TryAdd(key, result);

      return result;
    }
  }
}
