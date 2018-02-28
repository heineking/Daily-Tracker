using System.Collections.Generic;
using Commands.Contracts;
using Mediator.Contracts;
using System;
using Infrastructure.Errors;

namespace Commands.Events {
  public class CreateUser : IEvent, IValidatable<CreateUser> {
    public int UserId { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public void SetUserId(int id) {
      UserId = id;
    }

    public bool Validate(IValidator<CreateUser> validator, out IEnumerable<Error> brokenRules) {
      brokenRules = validator.BrokenRules(this);
      return validator.IsValid(this);
    }
  }
}
