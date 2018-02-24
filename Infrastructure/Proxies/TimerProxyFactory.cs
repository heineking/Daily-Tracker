using Infrastructure.Profiling;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Proxies {
  public class TimerProxyFactory {
    private IStopwatch _stopwatch;
    private ILogger _logger;

    public TimerProxyFactory(IStopwatch stopwatch, ILogger logger) {
      _stopwatch = stopwatch;
      _logger = logger;
    }

    public T Create<T>(T decorate) {
      object decorated = DispatchProxy.Create<T, MethodProxy<T>>();
      var proxy = ((MethodProxy<T>)decorated);
      proxy.Decorate(decorate);
      proxy.OnBeforeExecute += (t, e) => _stopwatch.Start();
      proxy.OnAfterExecute += (t, e) => {
        var elapsedMs = _stopwatch.Stop();
        _logger.Information("Executing {MethodName} Took {Elapsed} ms From {DeclaringTypeFullName}", e.Name, elapsedMs, e.DeclaringType.FullName);
      };
      proxy.OnErrorExecute += (t, e) => {
        _stopwatch.Stop();
      };
      return (T)decorated;
    }
  }
}
