using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions {
  public class InstanceNotFoundException : Exception {
    public InstanceNotFoundException(string message, Exception inner) : base(message, inner) {

    }
  }
}
