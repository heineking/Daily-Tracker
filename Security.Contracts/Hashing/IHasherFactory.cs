using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Contracts.Hashing {
  public interface IHasherFactory {
    IHasher Create(HashAlgorithm algorithm);
  }
}
