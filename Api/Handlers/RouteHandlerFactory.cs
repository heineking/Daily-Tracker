using Api.Handlers.Contracts;
using Commands.ValidationHandlers;
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

    public IRouteHandler CreateRouteHandler(NancyModule module) {
      var baseHandler = new RouteHandler(_hub, _validatorFactory, module);
      var withEtagSupport = new ETagHandler(baseHandler, module);
      return withEtagSupport;
    }
  }
}


