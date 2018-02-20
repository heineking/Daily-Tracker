using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator.Contracts {
  public interface IHub {
    void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    TResponse Send<TResponse>(IRequest<TResponse> request);
  }
}
