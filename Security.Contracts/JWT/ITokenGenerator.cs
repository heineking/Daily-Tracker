using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts.JWT {
  public interface ITokenGenerator {
    Token CreateFromUserId(int userId);
  }
}
