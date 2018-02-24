using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts {
  public interface IHashSettings {
    uint SaltLength { get; }
    uint Iteratations { get; }
    uint HashLength { get; }
  }
}
