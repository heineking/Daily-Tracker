using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Hashing {
  public class MD5HashAdapter : IHash {
    public string ComputeHash(string text) {
      var hashString = string.Empty;
      using (var md5 = MD5.Create()) {
        var textBytes = Encoding.UTF8.GetBytes(text);
        var hash = md5.ComputeHash(textBytes);
        hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
      }
      return hashString;
    }
  }
}
