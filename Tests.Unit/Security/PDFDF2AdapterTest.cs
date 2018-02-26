using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security;
using Security.Contracts;
using Security.Contracts.Hashing;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Unit.Security {
  [TestClass]
  public class PDFDF2AdapterTest {
    internal class HashSettings : IHashSettings {
      public int Iterations => 100;

      public int SaltBytes => 32;

      public int HashBytes => 32;
    }
    [TestMethod]
    public void Should_Generate_A_New_Hash_Each_Time_CreatePassword_Is_Passed_Text() {
      // arrange
      var text = "abcdefg";
      var hasher = new PDKDF2Adapter(new HashSettings());
      var pswd1 = hasher.CreatePassword(text);
      var pswd2 = hasher.CreatePassword(text);

      // act
      var hashesAreTheSame = pswd1.Hash.ToArray().IsEqualTo(pswd2.Hash.ToArray());
      var passwordsAreSame = pswd1.IsEqualTo(pswd2);

      // assert
      hashesAreTheSame.ShouldBeFalse();
      passwordsAreSame.ShouldBeFalse();
    }

    [TestMethod]
    public void Should_Return_Equal_Passwords_When_Passed_Hash_And_Salt_From_Original_Password() {
      // arrange
      var text = "abcde";
      var hasher = new PDKDF2Adapter(new HashSettings());
      var pswd = hasher.CreatePassword(text);
      var pswd2 = hasher.CreatePassword(text, pswd.Salt.ToArray(), pswd.Iterations);

      // act
      var result = pswd.IsEqualTo(pswd2);

      // assert
      result.ShouldBeTrue();
    }
  }
}
