using Commands.Contracts;
using Commands.Mapping;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Commands.EventHandlers {
  public static class EventHandlerExt {

    public static void ApplyPropertyUpdates(this IUpdateEvent updateEvent, object target) {
      var targetType = target.GetType();
      updateEvent.DirtyProperties.ForEach(prop => {
        if (Attribute.GetCustomAttribute(prop, typeof(MappingAttribute)) is MappingAttribute mappingAttr) {
          var property = targetType.GetProperty(mappingAttr.PropertyToName);
          property.SetValue(target, prop.GetValue(updateEvent));
        }
      });
    }
  }
}
