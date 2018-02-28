using Commands.Events;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Modules {
  public class UsersModule : NancyModule {
    public UsersModule(IHub hub) : base("/Users") {
      Post("/", _ => {
        var createUser = this.Bind<CreateUser>();
        hub.Publish(createUser);
        return Negotiate
          .WithStatusCode(HttpStatusCode.Accepted)
          .WithModel(new { id = createUser.UserId });
      });
    }
  }
}
