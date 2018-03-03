using Security.Contracts.JWT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.JWT {
  public class TokenSettings : ITokenSettings {
    public double ExpInHours => 8;

    public string Issuer => "DailyTracker.API";
  }
}
