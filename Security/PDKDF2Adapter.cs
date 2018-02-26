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

    public Password CreatePassword(string text, byte[] salt, int iterations) {
      var hash = GetBytes(text, salt, iterations);
      return new Password(hash, salt, iterations, Contracts.Hashing.HashAlgorithm.PDKDF2, PasswordConstants.Order);
    }

    public Password CreatePassword(string text) {
      var salt = Salt();
      var hash = GetBytes(text, salt, _settings.Iterations);
      return new Password(hash, salt, _settings.Iterations, Contracts.Hashing.HashAlgorithm.PDKDF2, PasswordConstants.Order);
    }

    private byte[] GetBytes(string text, byte[] salt, int iterations) {
      var bytes = new byte[_settings.HashBytes];
      using (var pdkf2 = new Rfc2898DeriveBytes(text, salt, iterations)) {
        bytes = pdkf2.GetBytes(_settings.HashBytes);
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
