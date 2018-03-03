using Commands.Events;
using Commands.ValidationHandlers;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.Validators {
  public class UniqueUsernameRule : IRule<CreateUser> {
    private readonly IRead<UserDirectory> _userDirectoryReader;

    public UniqueUsernameRule(IRead<UserDirectory> userDirectoryReader) {
      _userDirectoryReader = userDirectoryReader;
    }

    public string Message { get; set; }
    public Func<CreateUser, bool> IsValid { get { return Validate; } set { } }

    private bool Validate(CreateUser createUser) {
      var user = _userDirectoryReader
        .Where(ud => ud.Username.Equals(createUser.Username, StringComparison.InvariantCultureIgnoreCase))
        .FirstOrDefault();
      if (user == null) {
        return true;
      }
      Message = $"User {createUser.Username} already exists";
      return false;
    }
  }
}
