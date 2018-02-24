using Serilog;
using System.Reflection;

namespace Infrastructure.Proxies {
  public class LoggerProxyFactory {
    private readonly ILogger _logger;

    public LoggerProxyFactory(ILogger logger) {
      _logger = logger;
    }
    public T Create<T>(T implementation) {
      object decorated = DispatchProxy.Create<T, LoggerProxy<T>>();
      var proxy = ((LoggerProxy<T>)decorated);
      proxy.Decorate(implementation);
      proxy.OnBeforeExecute += (t, e) => _logger.Information("Before Executing {MethodName} from {DeclaringTypeFullName}", e.Name, e.DeclaringType.FullName);
      proxy.OnAfterExecute += (t, e) => _logger.Information("After Executing {MethodName} from {DeclaringTypeFullName}", e.Name, e.DeclaringType.FullName);
      proxy.OnErrorExecute += (t, e) => _logger.Error(e.Exception, "Error Executing {MethodName} from {DeclaringTypeFullName}", e.MethodInfo.Name, e.MethodInfo.DeclaringType.FullName);
      return (T)decorated;
    }
  }
}
