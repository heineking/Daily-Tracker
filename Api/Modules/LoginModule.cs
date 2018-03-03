using Commands.Events;
using Mediator.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Modules {
  public class LoginModule : NancyModule {
    public LoginModule(IHub hub) : base("/Login") {
      Post("/", _ => {
        var loginInformation = this.Bind<LoginUser>();

        var (isValid, token, updatedHash) = hub.Send(loginInformation);

        if (!isValid)
          return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);

        if (!string.IsNullOrEmpty(updatedHash))
          hub.Publish(new UpdateLoginInformation { Username = loginInformation.Username, Password = updatedHash });

        return Negotiate
          .WithStatusCode(HttpStatusCode.OK)
          .WithModel(new { token });
      });
    }
  }
}
