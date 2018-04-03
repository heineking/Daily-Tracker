using System;

namespace Infrastructure.Hashing {
  public interface IHash {
    string ComputeHash(string text);
  }
}
