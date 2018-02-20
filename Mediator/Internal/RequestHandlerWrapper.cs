using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator.Internal {

  internal abstract class RequestHandlerWrapper<TResponse> {
    public abstract TResponse Handle(IRequest<TResponse> request, SingleInstanceFactory singleFactory);
  }

  internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse> where TRequest : IRequest<TResponse> {
    public override TResponse Handle(IRequest<TResponse> request, SingleInstanceFactory singleFactory) {
      var handler = singleFactory(typeof(IRequestHandler<TRequest, TResponse>)) as IRequestHandler<TRequest, TResponse>;
      return handler.Handle((TRequest)request);
    }
  }
}
