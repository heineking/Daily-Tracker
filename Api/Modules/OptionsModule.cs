using Api.Handlers;
using Commands.Events;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Modules {
  public class OptionsModule : BaseModule {
    public OptionsModule(RouteHandlerFactory routeHandlerFactory) : base("/options") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);

      Get("/", _ => handler.Get<GetAllOptions, List<OptionModel>>(new GetAllOptions()));

      Post("/", _ => {
        this.RequiresAuthentication();

        var createOption = this.Bind<CreateOption>();
        createOption.SavedById = User.UserId;

        return handler.Post(createOption, (created) => new { id = created.OptionId });
      });

      Put("/{id:int}", _ => {
        this.RequiresAuthentication();

        var updateOption = this.Bind<UpdateOption>();
        updateOption.OptionId = _.id;
        updateOption.SavedById = User.UserId;

        return handler.Put(updateOption);
      });
    }
  }
}
