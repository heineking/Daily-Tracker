using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator.Contracts {
  public interface IEventHandler<in TEvent> where TEvent : IEvent {
    void Handle(TEvent @event);
  }
}
