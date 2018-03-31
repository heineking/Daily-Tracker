﻿using Commands.ValidationHandlers;
using Mediator.Contracts;
using Nancy;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Handlers {
  public class RouteHandlerFactory {
    private IHub _hub;
    private ValidatorFactory _validatorFactory;

    public RouteHandlerFactory(IHub hub, ValidatorFactory validatorFactory) {
      _hub = hub;
      _validatorFactory = validatorFactory;
    }

    public RouteHandler CreateRouteHandler(NancyModule module) {
      return new RouteHandler(_hub, _validatorFactory, module);
    }

    public class RouteHandler {
      private readonly IHub _hub;
      private readonly ValidatorFactory _validatorFactory;
      private readonly NancyModule _module;

      public RouteHandler(IHub hub, ValidatorFactory validatorFactory, NancyModule module) {
        _hub = hub;
        _validatorFactory = validatorFactory;
        _module = module;
      }

      public dynamic Get<TReq, TRes>(Func<TReq> createRequestModel) where TReq : IRequest<TRes> {
        var requestModel = createRequestModel();
        return _module
          .Negotiate
          .WithStatusCode(HttpStatusCode.OK)
          .WithModel(_hub.Send(requestModel));
      }

      public dynamic Post<TReq>(Func<TReq> createRequestModel, Func<TReq, object> createResponseModel) where TReq : class, IEvent {
        var requestModel = createRequestModel();

        var validator = _validatorFactory.CreateValidator<TReq>();
        var errors = validator.Validate(requestModel);

        if (errors.Any())
          return _module
            .Negotiate
            .WithStatusCode(HttpStatusCode.BadRequest)
            .WithModel(new { errors });

        _hub.Publish(requestModel);

        return _module
          .Negotiate
          .WithStatusCode(HttpStatusCode.Created)
          .WithModel(createResponseModel(requestModel));
      }

      public dynamic Put<TReq>(Func<TReq> createRequestModel) where TReq : class, IEvent {
        var requestModel = createRequestModel();

        var validator = _validatorFactory.CreateValidator<TReq>();
        var errors = validator.Validate(requestModel);

        if (errors.Any())
          return _module
            .Negotiate
            .WithStatusCode(HttpStatusCode.BadRequest)
            .WithModel(new { errors });

        _hub.Publish(requestModel);

        return _module
          .Negotiate
          .WithStatusCode(HttpStatusCode.NoContent);
      }

      public dynamic Delete<TReq>(Func<TReq> createRequestModel) where TReq : class, IEvent {
        var requestModel = createRequestModel();

        var validator = _validatorFactory.CreateValidator<TReq>();
        var errors = validator.Validate(requestModel);

        if (errors.Any())
          return _module
            .Negotiate
            .WithStatusCode(HttpStatusCode.BadRequest)
            .WithModel(new { errors });

        _hub.Publish(requestModel);

        return _module
           .Negotiate
           .WithStatusCode(HttpStatusCode.NoContent);
      }
    }
  }
}
