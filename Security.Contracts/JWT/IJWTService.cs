using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts.JWT {
  public interface IJWTService {
    Token DecodeToken(string token);
    string EncodeToken(Token token);
  }
}
