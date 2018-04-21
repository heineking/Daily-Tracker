using Commands.Contracts;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Commands.Events {
  public class UpdateOption : SaveOption, IEvent, IUpdateEvent {
    public List<PropertyInfo> DirtyProperties { get; set; }
    public UpdateOption() {
      DirtyProperties = new List<PropertyInfo>();
    }
  }
}
