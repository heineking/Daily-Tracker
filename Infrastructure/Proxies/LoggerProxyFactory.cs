using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Proxies {
  public class LoggerProxyFactory {
    public T Create<T>(T implementation) {
      object decorated = DispatchProxy.Create<T, LoggerProxy<T>>();
      var proxy = ((LoggerProxy<T>)decorated);
      proxy.Decorate(implementation);
      return (T)decorated;
    }
  }
}
