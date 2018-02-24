using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Proxies {
  public class LoggerProxy<TDecorated> : DispatchProxy {
    private TDecorated _decorated;
    private Predicate<MethodInfo> _filter;

    public event EventHandler<MethodInfo> OnBeforeExecute;
    public event EventHandler<MethodInfo> OnAfterExecute;
    public event EventHandler<MethodInfo> OnErrorExecute;
    public Predicate<MethodInfo> Filter {
      get { return _filter != null ? _filter : (_ => true); }
      set { _filter = value; }
    }
    public void Decorate(TDecorated decorated) {
      _decorated = decorated;
    }

    private Action<EventHandler<MethodInfo>> CreateEventHandlerTrigger(Func<bool> predicate, MethodInfo methodInfo) {
      return (handlers) => {
        if (handlers != null && predicate())
          handlers(this, methodInfo);
      };
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args) {
      var eventHandlerRunner = CreateEventHandlerTrigger(() => _filter(targetMethod), targetMethod);

      eventHandlerRunner(OnBeforeExecute);
      try {
        var result = targetMethod.Invoke(_decorated, args);
        eventHandlerRunner(OnAfterExecute);
        return result;
      } catch (Exception ex) {
        eventHandlerRunner(OnErrorExecute);
        throw ex;
      }
    }
  }
}
