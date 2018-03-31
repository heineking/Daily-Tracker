using Api.Handlers;
using Nancy;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Modules {
  public class ETagModule : NancyModule {
    public ETagModule(RouteHandlerFactory routeHandlerFactory) : base("/etag") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);
      Get("/{total:int}", _ => handler.Get<GetETagExample, List<UserModel>>(new GetETagExample { Total = _.total }));
      Get("/", _ => handler.Get<GetETagExample, List<UserModel>>(new GetETagExample { Total = 1000 }));
    }
  }
}
