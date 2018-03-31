using Api.Handlers.Contracts;
using System;
using System.Linq;
using Mediator.Contracts;
using Nancy.Responses.Negotiation;
using Nancy;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;

namespace Api.Handlers
{
  public class ETagHandler : IRouteHandler {
    private readonly IRouteHandler _delegate;
    private readonly NancyModule _module;

    public ETagHandler(IRouteHandler routeHandler, NancyModule module) {
      _delegate = routeHandler;
      _module = module;
    }

    public Negotiator Get<TReq, TRes>(TReq requestModel) where TReq : IRequest<TRes> {
      var response = _delegate.Get<TReq, TRes>(requestModel);
      response.WithHeader("Cache-Control", "public, max-age=600");

      using (var md5Hash = MD5.Create()) {

        var serialized = (string)JsonConvert.SerializeObject(response.NegotiationContext.DefaultModel);
        var serializedBytes = Encoding.UTF8.GetBytes(serialized);
        var hash = md5Hash.ComputeHash(serializedBytes);
        var hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

        if (!string.IsNullOrEmpty(RequestETag) && hashString == RequestETag) {
          return response
            .WithModel(null)
            .WithStatusCode(HttpStatusCode.NotModified);
        }

        return response
          .WithHeader("ETag", hashString);
      }
    }

    Negotiator IRouteHandler.Delete<TReq>(TReq requestModel) {
      return _delegate.Delete(requestModel);
    }

    Negotiator IRouteHandler.Post<TReq>(TReq requestModel, Func<TReq, object> createResponseModel) {
      return _delegate.Post(requestModel, createResponseModel);
    }

    Negotiator IRouteHandler.Put<TReq>(TReq requestModel) {
      return _delegate.Put(requestModel);
    }

    private string RequestETag => _module.Context.Request.Headers["If-None-Match"].FirstOrDefault();
  }
}
