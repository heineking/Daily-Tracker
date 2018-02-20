using Mediator.Contracts;
using Mediator.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediator {
  public class Hub : IHub {
    private readonly MultiInstanceFactory _multiInstanceFactory;
    private readonly SingleInstanceFactory _singleInstanceFactory;

    public Hub(MultiInstanceFactory multiInstanceFactory, SingleInstanceFactory singleInstanceFactory) {
      _multiInstanceFactory = multiInstanceFactory;
      _singleInstanceFactory = singleInstanceFactory;
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent {
      _multiInstanceFactory(typeof(IEventHandler<TEvent>))
        .Cast<IEventHandler<TEvent>>()
        .ToList()
        .ForEach(x => x.Handle(@event));
    }

    public TResponse Send<TResponse>(IRequest<TResponse> request) {
      var requestType = request.GetType();
      var handler = (RequestHandlerWrapper<TResponse>)Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResponse)));
      return handler.Handle(request, _singleInstanceFactory);
    }
  }
}
