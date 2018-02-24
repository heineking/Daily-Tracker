using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Security;
using Security.Contracts;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Unit.Security {
  class HashSettings : IHashSettings {
    public uint SaltLength { get; set; }
    public uint Iteratations { get; set; }
    public uint HashLength { get; set; }
    public HashSettings() {
      SaltLength = 16;
      Iteratations = 100;
      HashLength = 48;
    }
  }
  [TestClass]
  public class HasherTest {
    private readonly IHasher _hasher;
    public HasherTest() {
      var settings = new HashSettings();
      _hasher = new Hasher(settings);
    }

    [TestMethod]
    public void Generate_Hash_Should_Create_A_Unique_Hash_When_Passed_The_Same_Seed_Str() {
      // arrange
      var str = "abcdefghi";

      // act
      var hash1 = _hasher.GenerateHash(str);
      var hash2 = _hasher.GenerateHash(str);

      // assert
      hash1.ShouldNotBe(hash2);
    }

    [TestMethod]
    public void Generate_Hash_Should_Be_Able_To_Handle_Special_Characters() {
      // arrange
      var specialChars = @"!@#$%^&*()_+|}{[]\';:/.,./`~";

      // act
      _hasher.GenerateHash(specialChars);
    }

    [TestMethod]
    public void Validate_Should_Return_True_If_Plain_Text_String_Against_Hash_Created_With_String() {
      // arrange
      var str = "abcdefghi";
      var hash = _hasher.GenerateHash(str);

      // act
      var result = _hasher.Validate(hash, str);

      // assert
      result.ShouldBeTrue();
    }

    [TestMethod]
    public void Validate_Should_Return_False_If_Passed_Plain_Text_String_Against_Hash_Not_Created_With_String() {
      // arrange
      var str1 = "abcdef";
      var str2 = "abcdefg";
      var hash1 = _hasher.GenerateHash(str1);

      // act
      var result = _hasher.Validate(hash1, str2);

      // assert
      result.ShouldBeFalse();
    }
  }
}
