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
        var loginInformation = this.Bind<ValidateLogin>();
        var (isValid, updatedHash) = hub.Send(loginInformation);
        
        return Negotiate
          .WithStatusCode(isValid ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
      });
    }
  }
}
