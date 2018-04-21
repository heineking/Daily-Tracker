using Commands.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using Shouldly;
using Commands.Mapping;
using Commands.EventHandlers;

namespace Tests.Unit.Events {
  internal class Target {
    public string Foo2 { get; set; }
  }


  internal class Source : IUpdateEvent {

    [Mapping(nameof(Target.Foo2))]
    public string Foo1 { get; set; }

    public List<PropertyInfo> DirtyProperties { get; set; }

    public Source () {
      DirtyProperties = new List<PropertyInfo>();
    }
  }

  [TestClass]
  public class EventHandlerExtTests {
    
    [TestMethod]
    public void Should_Map_Dirty_Props_From_Source_to_Target() {
      // arrange
      var target = new Target();

      var source = new Source {
        Foo1 = "bar"
      };

      source.DirtyProperties.Add(typeof(Source).GetProperty(nameof(Source.Foo1)));

      // act
      source.ApplyPropertyUpdates(target);

      // assert
      target.Foo2.ShouldBe(source.Foo1);
    }
    
  }
}
