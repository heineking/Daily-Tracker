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
  }
}
