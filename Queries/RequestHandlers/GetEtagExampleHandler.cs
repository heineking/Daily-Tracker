using Mediator.Contracts;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.RequestHandlers {
  public class GetEtagExampleHandler : IRequestHandler<GetETagExample, List<UserModel>> {
    public List<UserModel> Handle(GetETagExample request) {
      var users = new List<UserModel>();
      for(var i = 0; i < request.Total; ++i) {
        users.Add(new UserModel {
          FirstName = $"FirstName{i}",
          LastName = $"LastName{i}",
          UserId = i
        });
      }
      return users;
    }
  }
}
