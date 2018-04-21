using Commands.Contracts;
using System.Linq;
using System.Reflection;

namespace Commands.Proxies {
  public class DirtyFlagProxy<T> : DispatchProxy where T : IUpdateEvent {

    public T Proxied { get; protected set; }

    public void Decorate(T implementation) {
      Proxied = implementation;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args) {
      var property = targetMethod.DeclaringType.GetProperties().FirstOrDefault(prop => prop.GetSetMethod() == targetMethod);
      if (property != null) {
        Proxied.DirtyProperties.Add(property);
      }
      return targetMethod.Invoke(Proxied, args);
    }
  }
}
