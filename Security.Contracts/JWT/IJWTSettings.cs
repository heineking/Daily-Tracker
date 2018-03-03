using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts.JWT {
  public interface IJWTSettings {
    string Key { get; }
  }
}
