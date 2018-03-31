using Mediator.Contracts;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Handlers.Contracts {
  public interface IRouteHandler {
    Negotiator Get<TReq, TRes>(TReq requestModel) where TReq : IRequest<TRes>;
    Negotiator Post<TReq>(TReq requestModel, Func<TReq, object> createResponseModel) where TReq : class, IEvent;
    Negotiator Put<TReq>(TReq requestModel) where TReq : class, IEvent;
    Negotiator Delete<TReq>(TReq requestModel) where TReq : class, IEvent;
  }
}
