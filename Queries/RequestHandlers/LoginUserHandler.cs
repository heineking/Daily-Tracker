using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using Security.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queries.RequestHandlers {
  public class LoginUserHandler : IRequestHandler<LoginUser, Tuple<bool, string>> {
    private IRead<UserDirectory> _userDirectoryReader;
    private PasswordService _passwordService;

    public LoginUserHandler(IRead<UserDirectory> userDirectoryReader, PasswordService passwordService) {
      _userDirectoryReader = userDirectoryReader;
      _passwordService = passwordService;
    }

    public Tuple<bool, string> Handle(LoginUser request) {
      var user = _userDirectoryReader.Where(ud => ud.Username.Equals(request.Username, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
      if (user != null && _passwordService.IsPasswordValid(request.Password, user.Password, out string updatedPasswordHash))
        return new Tuple<bool, string>(true, updatedPasswordHash);

      return new Tuple<bool, string>(false, String.Empty);
    }
  }
}
