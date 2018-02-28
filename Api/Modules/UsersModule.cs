using Commands.Contracts;
using Commands.Events;
using Commands.Validators;
using Infrastructure.Errors;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Modules {
  public class UsersModule : NancyModule {
    public UsersModule(IHub hub, IValidatorHandler validatorHandler) : base("/Users") {
      Post("/", _ => {
        var createUser = this.Bind<CreateUser>();
        
        var (isValid, errors) = validatorHandler.IsValid<CreateUserValidator, CreateUser>(createUser);
        
        if (isValid) {
          hub.Publish(createUser);

          return Negotiate
            .WithStatusCode(HttpStatusCode.Accepted)
            .WithModel(new { id = createUser.UserId });
        }
        return Negotiate
          .WithStatusCode(HttpStatusCode.BadRequest)
          .WithModel(new { errors });
      });
    }
  }
}
