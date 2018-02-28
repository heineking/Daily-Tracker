using Security.Contracts.Hashing;
using System;
using Security.Contracts;
using System.Security.Cryptography;

namespace Security {
  public class PDKDF2Adapter : IHasher {

    private readonly IHashSettings _settings;

    public PDKDF2Adapter(IHashSettings settings) {
      _settings = settings;
    }

    public Password CreatePassword(string text, byte[] salt, int hashBytes, int iterations) {
      var hash = GetBytes(text, salt, hashBytes, iterations);
      return new Password(hash, salt, iterations, Contracts.Hashing.HashAlgorithm.PDKDF2);
    }

    public Password CreatePassword(string text) {
      var salt = Salt();
      var hash = GetBytes(text, salt, _settings.HashBytes, _settings.Iterations);
      return new Password(hash, salt, _settings.Iterations, Contracts.Hashing.HashAlgorithm.PDKDF2);
    }

    private byte[] GetBytes(string text, byte[] salt, int hashBytes, int iterations) {
      var bytes = new byte[hashBytes];
      using (var pdkf2 = new Rfc2898DeriveBytes(text, salt, iterations)) {
        bytes = pdkf2.GetBytes(hashBytes);
      }
      return bytes;
    }
    
    private byte[] Salt() {
      var salt = new byte[_settings.SaltBytes];
      using (var crypto = new RNGCryptoServiceProvider()) {
        crypto.GetBytes(salt);
      }
      return salt;
    }
  }
}
