using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.Contracts;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Unit.Security {
  [TestClass]
  public class ByteArrayExtensionTests {

    [TestMethod]
    public void Split_Should_Split_Byte_Array_Into_Two_Byte_Arrays() {
      // arrange
      var byteLen = 10;
      var bytes = new byte[byteLen];
      var rnd = new Random();
      rnd.NextBytes(bytes);

      // act
      var (lhs, rhs) = bytes.Split(5);

      // assert
      lhs.ShouldNotBeNull();
      lhs.Length.ShouldBe(5);
      rhs.ShouldNotBeNull();
      rhs.Length.ShouldBe(5);
    }

    [TestMethod]
    public void Split_Should_Not_Change_The_Contents_Of_Byte_Array() {
      // arrange
      var rnd = new Random();
      var byteLen = 10;
      var bytes = new byte[byteLen];
      rnd.NextBytes(bytes);

      // act
      var (lhs, rhs) = bytes.Split(5);

      // assert
      for(int i = 0, j = 0; i < byteLen; ++i, ++j) {
        var lookingAtByte = bytes[i];
        var comparingTo = i < 5 ? lhs[j] : rhs[j];
        if (lookingAtByte != comparingTo) {
          throw new Exception("Byte was changed.");
        }
        
        // reset the byte index
        if (i == 4)
          j = -1;
      }
    }

    [TestMethod]
    public void IsEqualTo_Should_Return_True_If_Two_Byte_Arrays_Are_Equal() {
      // arrange
      var rnd = new Random();
      var bytes1 = new byte[5];
      var bytes2 = new byte[5];
      rnd.NextBytes(bytes1);
      Array.Copy(bytes1, bytes2, 5);

      // act
      var result = bytes1.IsEqualTo(bytes2);

      // assert
      result.ShouldBeTrue();
    }

    [TestMethod]
    public void IsEqualTo_Should_Return_False_If_Two_Byte_Arrays_Are_Not_Equal() {
      // arrange
      var rnd = new Random();
      var bytes1 = Convert.FromBase64String("abcd");
      var bytes2 = Convert.FromBase64String("dcba");

      // act
      var result = bytes1.IsEqualTo(bytes2);

      // assert
      result.ShouldBeFalse();
    }

    [TestMethod]
    public void CombineWith_Should_Return_A_Combined_Byte_Array() {
      // arrange
      var bytes = Convert.FromBase64String("abcdefgh");
      var (lhs, rhs) = bytes.Split(4);

      // act
      var combined = lhs.CombineWith(rhs);

      // assert
      combined.IsEqualTo(bytes).ShouldBeTrue();
    }
  }
}
