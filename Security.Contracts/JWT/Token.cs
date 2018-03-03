using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts.JWT {
  public class Token {
    // JWT Reserved claims
    public string Iss { get; set; }
    public DateTime Exp { get; set; }

    // application specific
    public string Username { get; set; }
    public int UserId { get; set; }
  }
}
