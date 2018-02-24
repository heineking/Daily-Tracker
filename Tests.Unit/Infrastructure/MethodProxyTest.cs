using Infrastructure.Proxies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Tests.Unit.Infrastructure {
  [TestClass]
  public class MethodProxyTest {

    public class Foo : IFoo {
      public void Bar() {
        throw new NotImplementedException();
      }

      public void Baz() {
        throw new NotImplementedException();
      }
    }

    public interface IFoo {
      void Baz();
      void Bar();
    }

    private readonly IFoo _proxiedDecorator;
    private readonly MethodProxy<IFoo> _proxy;

    public MethodProxyTest() {
      object decorated = DispatchProxy.Create<IFoo, MethodProxy<IFoo>>();
      _proxy = ((MethodProxy<IFoo>)decorated);
      _proxiedDecorator = (IFoo)_proxy;
    }

    [TestMethod]
    public void MethodProxy_Decorate_Should_Wrap_Concrete_Implementation_Into_Proxied_Decorator() {
      // arrange
      var mock = new Mock<IFoo>();
      mock.Setup(foo => foo.Bar());

      // act
      _proxy.Decorate(mock.Object);
      _proxiedDecorator.Bar();

      // assert
      mock.Verify(foo => foo.Bar());
    }

    [TestMethod]
    public void MethodProxy_Null_Filter_Should_Return_Function_That_Returns_True() {
      // arrange
      var filter = _proxy.Filter;
      var barMethodInfo = typeof(Foo).GetMethod("Bar");

      // act
      var result = filter(barMethodInfo);

      // assert
      Assert.IsTrue(result);
    }
  }
}
