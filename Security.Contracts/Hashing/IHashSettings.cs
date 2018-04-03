using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts.Hashing {
  public enum HashParts {
    Iterations,
    Salt,
    Hash,
    Algorithm
  }

  public enum HashAlgorithm {
    PBKDF2
  }

  public interface IHashSettings {
    int Iterations { get; }
    int SaltBytes { get; }
    int HashBytes { get; }
    HashAlgorithm Algorithm { get; }
  }
}
