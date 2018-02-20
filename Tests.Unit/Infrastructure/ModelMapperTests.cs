using Infrastructure.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Unit.Infrastructure {

  class Foo {
    public int Int { get; set; }
    public string Str { get; set; }
    public DateTime Date { get; set; }
    public List<String> Strs { get; set; }
  }

  [TestClass]
  public class ModelMapperTests {
    private readonly IModelMapper _mapper;

    public ModelMapperTests() {
      _mapper = new ModelMapper();
    }

    [TestMethod]
    public void Clone_Method_Should_Return_New_Object_With_Copied_Public_Properties() {

      // arrange
      var foo = new Foo {
        Date = DateTime.Parse("2018-02-20"),
        Int = 1,
        Str = "Bar",
        Strs = new List<string> { "Foo", "Bar" }
      };

      // act
      var cloned = _mapper.ShallowClone(foo);
      
      // assert
      Assert.IsFalse(ReferenceEquals(foo, cloned));
      Assert.IsTrue(ReferenceEquals(foo.Strs, cloned.Strs));
      cloned.Int.ShouldBe(foo.Int);
      cloned.Str.ShouldBe(foo.Str);
      cloned.Date.ShouldBe(foo.Date);
    }
  }
}
