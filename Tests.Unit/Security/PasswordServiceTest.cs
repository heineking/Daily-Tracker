using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security;
using Security.Contracts;
using Security.Contracts.Hashing;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Unit.Security {
  [TestClass]
  public class PasswordServiceTest {
    class HashSettings : IHashSettings {
      public int Iterations { get; }

      public int SaltBytes { get; }

      public int HashBytes { get; }

      public HashAlgorithm Algorithm { get; }

      public HashSettings(int iterations, int saltBytes, int hashBytes, HashAlgorithm algorithm) {
        Iterations = iterations;
        SaltBytes = saltBytes;
        HashBytes = hashBytes;
        Algorithm = algorithm;
      }
    }

    [TestMethod]
    public void Should_Return_True_If_Passed_String_That_Equals_Existing_Password() {
      // arrange
      var passwordPlainText = "p@$$w0rd1";
      var hasherFactory = new HasherFactory(new HashSettings(100, 32, 32, HashAlgorithm.PBKDF2));
      var hasher = hasherFactory.Create();
      var password = hasher.CreatePassword(passwordPlainText);
      var passwordService = new PasswordService(hasherFactory);

      // act
      var result = passwordService.IsPasswordValid(passwordPlainText, password);

      // assert
      result.ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Return_False_If_Passed_String_That_Does_Not_Equal_Existing_Password() {
      // arrange
      var passwordPlainText = "p@$$w0rd1";
      var hasherFactory = new HasherFactory(new HashSettings(100, 32, 32, HashAlgorithm.PBKDF2));
      var hasher = hasherFactory.Create();
      var password = hasher.CreatePassword(passwordPlainText);
      var passwordService = new PasswordService(hasherFactory);

      // act
      var result = passwordService.IsPasswordValid($"{passwordPlainText}1", password);

      // assert
      result.ShouldBeFalse();
    }

    [TestMethod]
    public void Should_Output_New_Password_Hash_If_Settings_Have_Changed_Since_Original_Was_Generated() {
      // arrange
      var passwordPlainText = "p@$$w0rd1";
      var hasherFactory = new HasherFactory(new HashSettings(100, 32, 32, HashAlgorithm.PBKDF2));
      var hasher = hasherFactory.Create();
      var password = hasher.CreatePassword(passwordPlainText);
      var passwordService = new PasswordService(hasherFactory);

      var hashFactoryWithupdatedSettings = new HasherFactory(new HashSettings(1000, 8, 24, HashAlgorithm.PBKDF2));
      var passwordServiceUpdated = new PasswordService(hashFactoryWithupdatedSettings);

      // act
      var hash = String.Empty;
      var isValid = passwordServiceUpdated.IsPasswordValid(passwordPlainText, password, out hash);

      // assert
      hash.ShouldNotBeNullOrEmpty();
      isValid.ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Not_Output_New_Password_Hash_If_Settings_Have_Not_Changed() {
      // arrange
      var passwordPlainText = "p@$$w0rd1";
      var hasherFactory = new HasherFactory(new HashSettings(100, 32, 32, HashAlgorithm.PBKDF2));
      var hasher = hasherFactory.Create();
      var password = hasher.CreatePassword(passwordPlainText);
      var passwordService = new PasswordService(hasherFactory);
      
      // act
      var hash = String.Empty;
      var isValid = passwordService.IsPasswordValid(passwordPlainText, password, out hash);

      // assert
      hash.ShouldBeNullOrEmpty();
      isValid.ShouldBeTrue();
    }
  }
}
