using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts {
  public interface IHasher {
    string GenerateHash(string str);
    bool Validate(string hash, string str);
  }
}
