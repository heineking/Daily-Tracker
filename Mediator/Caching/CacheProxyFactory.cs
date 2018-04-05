using Mediator.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mediator.Caching {

  public class Cache {
    public readonly ConcurrentDictionary<string, dynamic> _dict;
    public Cache() {
      _dict = new ConcurrentDictionary<string, dynamic>();
    }
  }

  public abstract class ProxyFactoryWrapper<TRequest, TResponse> where TRequest : IRequest<TResponse> {
    public abstract IRequestHandler<TRequest, TResponse> Create(IRequestHandler<TRequest, TResponse> implementation, Cache cache);
  }

  public class CacheProxyFactoryWrapperImpl<TRequest, TResponse> : ProxyFactoryWrapper<TRequest, TResponse> where TRequest : IRequest<TResponse> {

    public override IRequestHandler<TRequest, TResponse> Create(IRequestHandler<TRequest, TResponse> implementation, Cache cache) {
      object decorated = DispatchProxy.Create<IRequestHandler<TRequest, TResponse>, CacheProxy<IRequestHandler<TRequest, TResponse>>>();
      var proxy = (CacheProxy<IRequestHandler<TRequest, TResponse>>)decorated;
      proxy.Decorate(implementation);
      proxy.SetCache(cache);
      return (IRequestHandler<TRequest, TResponse>)proxy;
    }
  }

  public class CacheProxyFactory {
    private readonly Cache _cache;

    public CacheProxyFactory(Cache cache) {
      _cache = cache;
    }

    public object Create(object serviceType) {

      var type = serviceType.GetType();
      var requestHandlerType = type.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

      if (requestHandlerType == null)
        return serviceType;

      var argumentTypes = requestHandlerType.GetGenericArguments();
      var requestType = argumentTypes[0];
      var responseType = argumentTypes[1];

      var cacheProxyFactory = Activator.CreateInstance(typeof(CacheProxyFactoryWrapperImpl<,>).MakeGenericType(requestType, responseType));
      var createProxy = cacheProxyFactory.GetType().GetMethod("Create");
      var proxied = createProxy.Invoke(cacheProxyFactory, new[] { serviceType, _cache });

      return proxied;
    }
  }
}
