using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.Contracts;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Security.Contracts.Hashing;

namespace Tests.Unit.Security {
  [TestClass]
  public class PasswordTest {
    
    [TestMethod]
    public void Should_Correctly_Parse_A_Delimited_Into_A_Constructed_Password() {
      // arrange
      var str = "0:10000:abcd:efgh";

      // act
      var password = new Password(str);

      // assert
      password.Algorithm.ShouldBe(HashAlgorithm.PBKDF2);
      password.Iterations.ShouldBe(10000);
      password.Hash.ShouldBe(Convert.FromBase64String("abcd"));
      password.Salt.ShouldBe(Convert.FromBase64String("efgh"));
    }
    
    [TestMethod]
    public void Calling_To_String_Should_Return_The_Same_String_That_Was_Passed_In() {
      // arrange
      var str = "0:100:abcd:efgh";
      var pswd = new Password(str);

      // act
      var result = pswd.ToString();

      // assert
      result.ShouldBe(str);
    }

    [TestMethod]
    public void Should_Return_True_If_Given_Password_That_Was_Created_With_Same_Hash() {
      // arrange
      var str = "0:100:abcd:efgh";
      var str2 = "0:100:abcd:ijkl";

      var pswd = new Password(str);
      var pswd2 = new Password(str2);

      // act
      var result = pswd.IsEqualTo(pswd2);

      // assert
      result.ShouldBeTrue();
    }
  }
}
