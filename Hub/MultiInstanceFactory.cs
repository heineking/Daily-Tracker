using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator {
  public delegate IEnumerable<object> MultiInstanceFactory(Type serviceType);
}
