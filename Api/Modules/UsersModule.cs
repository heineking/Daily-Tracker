using Api.Handlers;
using Commands.Events;
using Nancy;
using Nancy.ModelBinding;

namespace Api.Modules {
  public class UsersModule : NancyModule {
    public UsersModule(RouteHandlerFactory routeHandlerFactory) : base("/Users") {
      var handler = routeHandlerFactory.CreateRouteHandler(this);

      Post("/", _ => {
        return handler.Post(createRequest, createResponse);

        CreateUser createRequest() {
          var createUser = this.Bind<CreateUser>();
          return createUser;
        }

        object createResponse(CreateUser createUser) {
          return new { id = createUser.UserId };
        }
      });
    }
  }
}
