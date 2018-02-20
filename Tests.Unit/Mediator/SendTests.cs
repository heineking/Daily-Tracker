using Mediator;
using Mediator.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using StructureMap;

namespace Tests.Unit.Mediator {
  [TestClass]
  public class SendTests {
    public class Ping : IRequest<Pong> {
      public string Message = "Ping";
    }

    public class Pong {
      public string Message { get; set; }
    }

    public class PingHandler : IRequestHandler<Ping, Pong> {
      public Pong Handle(Ping request) {
        return new Pong {
          Message = $"{request.Message} Pong"
        };
      }
    }

    [TestMethod]
    public void Should_Resolve_Main_Send_Handler() {
      var container = new Container(cfg => {
        cfg.Scan(scanner => {
          scanner.AssemblyContainingType(typeof(SendTests));
          scanner.IncludeNamespaceContainingType<Ping>();
          scanner.WithDefaultConventions();
          scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
        });
        cfg.For<IHub>().Use<Hub>();
        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
      });
      // arrange
      var hub = container.GetInstance<IHub>();

      // act
      var pong = hub.Send(new Ping());

      // assert
      pong.Message.ShouldBe("Ping Pong");
    }

  }
}
