using Api.Auth;
using Commands.Contracts;
using Commands.Proxies;
using Nancy;
using Nancy.Extensions;
using Nancy.IO;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Api.Modules {
  public class BaseModule : NancyModule {

    protected DailyTrackerPrincipal User => (DailyTrackerPrincipal)Context.CurrentUser;

    public BaseModule() : base("api/v1/") {

    }

    public BaseModule(string module) : base($"api/v1/{module.TrimStart('/')}") {

    }

    public TModel BindUpdateModel<TInterface, TModel>() where TModel : IUpdateEvent, TInterface {
      var body = RequestStream.FromStream(Request.Body).AsString();
      var model = (TModel)Activator.CreateInstance(typeof(TModel));

      object decorated = DispatchProxy.Create<TInterface, DirtyFlagProxy<TModel>>();
      var proxy = (DirtyFlagProxy<TModel>)decorated;
      proxy.Decorate(model);

      JsonConvert.PopulateObject(body, decorated);

      return proxy.Proxied;
    }
  }
}
