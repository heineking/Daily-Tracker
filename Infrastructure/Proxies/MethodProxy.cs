using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Proxies {

  public class ErrorEvent {
    public Exception Exception { get; set; }
    public MethodInfo MethodInfo { get; set; }
  }

  public class MethodProxy<TDecorated> : DispatchProxy {
    private TDecorated _decorated;
    private Predicate<MethodInfo> _filter;

    public event EventHandler<MethodInfo> OnBeforeExecute;
    public event EventHandler<MethodInfo> OnAfterExecute;
    public event EventHandler<ErrorEvent> OnErrorExecute;

    public Predicate<MethodInfo> Filter {
      get { return _filter != null ? _filter : (_ => true); }
      set { _filter = value; }
    }
    public void Decorate(TDecorated decorated) {
      _decorated = decorated;
    }

    private Action<EventHandler<T>, T> CreateEventHandlerRunner<T>(bool predicate) {
      return (handlers, e) => {
        if (handlers != null && predicate)
          handlers(this, e);
      };
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args) {
      var executeMethodInfoHandlers = CreateEventHandlerRunner<MethodInfo>(Filter(targetMethod));
      var executeErrorMethodInfoHandlers = CreateEventHandlerRunner<ErrorEvent>(Filter(targetMethod));

      executeMethodInfoHandlers(OnBeforeExecute, targetMethod);
      try {
        var result = targetMethod.Invoke(_decorated, args);
        executeMethodInfoHandlers(OnAfterExecute, targetMethod);
        return result;
      } catch (Exception ex) {
        executeErrorMethodInfoHandlers(OnErrorExecute, new ErrorEvent { Exception = ex, MethodInfo = targetMethod });
        throw ex;
      }
    }
  }
}
