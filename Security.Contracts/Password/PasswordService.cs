using Security.Contracts.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.Contracts {

  public class PasswordService {
    private readonly IHasherFactory _hasherFactory;
    public PasswordService(IHasherFactory hasherFactory) {
      _hasherFactory = hasherFactory;
    }

    public bool IsPasswordValid(string plainTextPassword, string passwordHash, out string updatedPasswordHash) {
      var password = new Password(passwordHash);
      return IsPasswordValid(plainTextPassword, password, out updatedPasswordHash);
    }

    public bool IsPasswordValid(string plainTextPassword, Password password) {
      var hasher = _hasherFactory.Create(password.Algorithm);
      var compareAgainst = hasher.CreatePassword(plainTextPassword, password.Salt.ToArray(), password.Hash.Count, password.Iterations);
      return password.IsEqualTo(compareAgainst);
    }

    public bool IsPasswordValid(string plainTextPassword, Password password, out string updated) {
      var isValid = IsPasswordValid(plainTextPassword, password);
      updated = String.Empty;

      if (!isValid)
        return false;

      var hasher = _hasherFactory.Create();
      var newPassword = hasher.CreatePassword(plainTextPassword);

      if (password.SettingsUpdated(newPassword)) {
        updated = newPassword.ToString();
      }

      return true;
    }
  }
}
