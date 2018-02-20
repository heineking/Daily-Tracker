using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator.Contracts {
  public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse> {
    TResponse Handle(TRequest request);
  }
}
