using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts.JWT {
  public interface ITokenSettings {
    double ExpInHours { get; }
    string Issuer { get; }
  }
}
