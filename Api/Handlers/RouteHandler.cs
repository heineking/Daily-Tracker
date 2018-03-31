using Api.Handlers.Contracts;
using Commands.ValidationHandlers;
using Mediator.Contracts;
using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Handlers
{
  public class RouteHandler : IRouteHandler {
    private readonly IHub _hub;
    private readonly ValidatorFactory _validatorFactory;
    private readonly NancyModule _module;

    public RouteHandler(IHub hub, ValidatorFactory validatorFactory, NancyModule module) {
      _hub = hub;
      _validatorFactory = validatorFactory;
      _module = module;
    }

    public Negotiator Get<TReq, TRes>(TReq requestModel) where TReq : IRequest<TRes> {
      return _module
        .Negotiate
        .WithStatusCode(HttpStatusCode.OK)
        .WithModel(_hub.Send(requestModel));
    }

    public Negotiator Post<TReq>(TReq requestModel, Func<TReq, object> createResponseModel) where TReq : class, IEvent {
      return Publish(requestModel) ?? _module
        .Negotiate
        .WithStatusCode(HttpStatusCode.Created)
        .WithModel(createResponseModel(requestModel));
    }

    public Negotiator Put<TReq>(TReq requestModel) where TReq : class, IEvent {
      return Publish(requestModel) ?? _module
        .Negotiate
        .WithStatusCode(HttpStatusCode.NoContent);
    }

    public Negotiator Delete<TReq>(TReq requestModel) where TReq : class, IEvent {
      return Publish(requestModel) ?? _module
         .Negotiate
         .WithStatusCode(HttpStatusCode.NoContent);
    }

    private Negotiator Publish<TReq>(TReq requestModel) where TReq : class, IEvent {

      var validator = _validatorFactory.CreateValidator<TReq>();
      var errors = validator.Validate(requestModel);

      if (errors.Any())
        return _module
          .Negotiate
          .WithStatusCode(HttpStatusCode.BadRequest)
          .WithModel(new { errors });

      _hub.Publish(requestModel);
      return null;
    }
  }
}
