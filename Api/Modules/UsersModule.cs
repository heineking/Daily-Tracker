using Api.Handlers;
using Commands.Events;
using Nancy;
using Nancy.ModelBinding;

namespace Api.Modules {
  public class UsersModule : BaseModule {
    public UsersModule(RouteHandlerFactory routeHandlerFactory) : base("/Users") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);

      Post("/", _ => {
        var createUser = this.Bind<CreateUser>();

        return handler.Post(createUser, createResponse);

        object createResponse(CreateUser created) {
          return new { id = created.UserId };
        }
      });
    }
  }
}
