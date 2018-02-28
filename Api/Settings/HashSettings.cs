using Security.Contracts.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Settings
{
  public class HashSettings : IHashSettings {
    public int Iterations => 1000;

    public int SaltBytes => 32;

    public int HashBytes => 32;

    public HashAlgorithm Algorithm => HashAlgorithm.PDKDF2;
  }
}
