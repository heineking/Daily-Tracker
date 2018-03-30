using Commands.Events;
using Commands.ValidationHandlers;
using Mediator.Contracts;
using Queries.Requests;
using System;

namespace Commands.Validators {
  public class UniqueUsernameRule : IRule<CreateUser> {
    private readonly IHub _hub;

    public UniqueUsernameRule(IHub hub) {
      _hub = hub;
    }

    public string Message { get; set; }
    public Func<CreateUser, bool> IsValid { get { return Validate; } set { } }

    private bool Validate(CreateUser createUser) {
      var user = _hub.Send(new GetUserByUsername { Username = createUser.Username });
      if (user == null) {
        return true;
      }
      Message = $"User {createUser.Username} already exists";
      return false;
    }
  }
}
