using Mediator.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Reflection;

namespace Mediator.Caching {

  public abstract class ProxyFactoryWrapper<TRequest, TResponse> where TRequest : IRequest<TResponse> {
    public abstract IRequestHandler<TRequest, TResponse> Create(IRequestHandler<TRequest, TResponse> implementation, IMemoryCache cache, uint cacheForSeconds, CacheType cacheType);
  }

  public class CacheProxyFactoryWrapperImpl<TRequest, TResponse> : ProxyFactoryWrapper<TRequest, TResponse> where TRequest : IRequest<TResponse> {
    public override IRequestHandler<TRequest, TResponse> Create(IRequestHandler<TRequest, TResponse> implementation, IMemoryCache cache, uint cacheForSeconds, CacheType cacheType) {
      object decorated = DispatchProxy.Create<IRequestHandler<TRequest, TResponse>, CacheProxy<IRequestHandler<TRequest, TResponse>>>();
      var proxy = (CacheProxy<IRequestHandler<TRequest, TResponse>>)decorated;
      proxy.Decorate(implementation);
      proxy.SetCache(cache);
      proxy.SetCacheDirectives(cacheForSeconds, cacheType);
      return (IRequestHandler<TRequest, TResponse>)proxy;
    }
  }

  public class CacheProxyFactory {
    private readonly IMemoryCache _cache;

    public CacheProxyFactory(IMemoryCache cache) {
      _cache = cache;
    }

    public object Create(object serviceType) {

      var type = serviceType.GetType();
      var cacheAttr = (CacheAttribute)Attribute.GetCustomAttribute(type, typeof(CacheAttribute));

      if (cacheAttr == null)
        return serviceType;

      var requestHandlerType = type.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

      if (requestHandlerType == null)
        return serviceType;

      var cacheForSeconds = cacheAttr.Seconds;
      var cacheType = cacheAttr.CacheType;

      var argumentTypes = requestHandlerType.GetGenericArguments();
      var requestType = argumentTypes[0];
      var responseType = argumentTypes[1];

      var cacheProxyFactory = Activator.CreateInstance(typeof(CacheProxyFactoryWrapperImpl<,>).MakeGenericType(requestType, responseType));
      var createProxy = cacheProxyFactory.GetType().GetMethod("Create");
      var proxied = createProxy.Invoke(cacheProxyFactory, new[] { serviceType, _cache, cacheForSeconds, cacheType });

      return proxied;
    }
  }
}
