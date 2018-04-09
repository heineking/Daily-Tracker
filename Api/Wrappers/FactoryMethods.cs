using Infrastructure.Exceptions;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Wrappers {
  public static class FactoryMethods {
    public static object Execute(Func<object> findService) {
      try {
        return findService();
      } catch (StructureMapConfigurationException ex) {
        throw new InstanceNotFoundException("Unable to resolve instance from container", ex);
      }
    }
  }
}
