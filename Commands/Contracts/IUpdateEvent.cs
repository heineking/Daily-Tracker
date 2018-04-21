using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Commands.Contracts {
  public interface IUpdateEvent {
    List<PropertyInfo> DirtyProperties { get; set; }
  }
}
