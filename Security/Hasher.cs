using Security.Contracts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Security {
  public class Hasher : IHasher {
    private readonly IHashSettings _settings;
    private readonly RNGCryptoServiceProvider _crypto;
    private readonly HMACSHA256 _hmacsha256;

    public Hasher(IHashSettings hashSettings) {
      _settings = hashSettings;
      _crypto = new RNGCryptoServiceProvider();
      _hmacsha256 = new HMACSHA256(Encoding.ASCII.GetBytes(_settings.Key));
    }

    public string GenerateHash(string str) {
      var salt = Salt;
      var hash = HashWithSalt(str, salt);
      return Convert.ToBase64String(hash.CombineWith(salt));
    }

    public bool Validate(string hashString, string str) {
      var hashBytes = Convert.FromBase64String(hashString);
      var (hash, salt) = hashBytes.Split(_settings.HashLength);
      var hashedStr = HashWithSalt(str, salt);
      return hash.IsEqualTo(hashedStr);
    }

    private byte[] HashWithSalt(string str, byte[] salt) {
      var pdkdf2 = new Rfc2898DeriveBytes(str, salt, (int)_settings.Iteratations);
      return pdkdf2.GetBytes((int)_settings.HashLength);
    }

    private byte[] Keyed(byte[] bytes) {
      return _hmacsha256.ComputeHash(bytes);
    }

    private byte[] Salt {
      get {
        byte[] salt;
        _crypto.GetBytes(salt = new Byte[_settings.SaltLength]);
        return salt;
      }
    }
  }
}
