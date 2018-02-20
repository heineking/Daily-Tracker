using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using System;
using System.Text;
using System.IO;
using Shouldly;
using Mediator.Contracts;
using Mediator;
using System.Collections.Generic;
using System.Threading;

namespace Tests.Unit.Mediator {

  [TestClass]
  public class PublishTests {

    public class Ping : IEvent {
      public string Message = "Ping";
    }

    public class Wiff : IEvent {
      public string Message = "Whoosh";
    }

    public class Pong : IEventHandler<Ping> {

      private readonly TextWriter _writer;

      public Pong(TextWriter writer) {
        _writer = writer;
      }

      public void Handle(Ping ping) {
        _writer.WriteLine($"{ping.Message} Pong");
      }
    }

    public class Pung : IEventHandler<Ping> {
      private readonly TextWriter _writer;
      private readonly IHub _hub;

      public Pung(TextWriter writer, IHub hub) {
        _writer = writer;
        _hub = hub;
      }

      public void Handle(Ping ping) {

        _writer.WriteLine($"{ping.Message} Pung");

        _hub.Publish(new Wiff());
      }
    }

    public class GetName : IRequest<string> { }

    public class GetNameHandler : IRequestHandler<GetName, string> {
      public string Handle(GetName request) {
        return "Foo Bar";
      }
    }

    public class Plop : IEventHandler<Wiff> {

      private readonly TextWriter _writer;

      public Plop(TextWriter writer) {
        _writer = writer;
      }

      public void Handle(Wiff @event) {
        _writer.WriteLine($"{@event.Message} Plop");
      }
    }

    public class Event1 : IEvent { }
    public class Event2 : IEvent { }

    public abstract class EventHandlerBase<TEvent> where TEvent : IEvent {
      public List<String> Executions { get; set; }

      public EventHandlerBase(List<string> executions) {
        Executions = executions;
      }

      public virtual void Handle(TEvent @event) {
        Executions.Add(typeof(TEvent).Name);
      }
    }

    public class Event2Handler : EventHandlerBase<Event2>, IEventHandler<Event2> {
      public Event2Handler(List<string> executions) : base(executions) {
      }
      public override void Handle(Event2 @event) {
        base.Handle(@event);
      }
    }

    public class Event1Handler : EventHandlerBase<Event1>, IEventHandler<Event1> {
      public Event1Handler(List<string> executions) : base(executions) {
      }
      public override void Handle(Event1 @event) {
        Thread.Sleep(10);
        base.Handle(@event);
      }
    }


    [TestMethod]
    public void Should_Resolve_Main_Publish_Handlers() {
      var builder = new StringBuilder();
      var writer = new StringWriter(builder);

      var container = new Container(cfg => {
        cfg.Scan(scanner => {
          scanner.AssemblyContainingType(typeof(PublishTests));
          scanner.IncludeNamespaceContainingType<Ping>();
          scanner.WithDefaultConventions();
          scanner.AddAllTypesOf(typeof(IEventHandler<>));
        });
        cfg.For<TextWriter>().Use(writer);
        cfg.For<IHub>().Use<Hub>();
        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
      });

      // arrange
      var hub = container.GetInstance<IHub>();

      // act
      hub.Publish(new Ping());

      // assert
      var result = builder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      result.ShouldContain("Ping Pong");
      result.ShouldContain("Ping Pung");
      result.ShouldContain("Whoosh Plop");
    }

    [TestMethod]
    public void Should_Resolve_Events_In_Sequential_Order() {
      var executions = new List<string>();

      var container = new Container(cfg => {
        cfg.Scan(scanner => {
          scanner.AssemblyContainingType(typeof(PublishTests));
          scanner.IncludeNamespaceContainingType<Ping>();
          scanner.WithDefaultConventions();
          scanner.AddAllTypesOf(typeof(IEventHandler<>));
        });
        cfg.For<List<string>>().Use(executions);
        cfg.For<IHub>().Use<Hub>();
        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
      });

      // arrange
      var hub = container.GetInstance<IHub>();

      // act
      hub.Publish(new Event1());
      hub.Publish(new Event2());

      // assert
      Assert.AreEqual(executions.Count, 2);
      executions[0].ShouldBe(typeof(Event1).Name);
      executions[1].ShouldBe(typeof(Event2).Name);
    }
  }
}
