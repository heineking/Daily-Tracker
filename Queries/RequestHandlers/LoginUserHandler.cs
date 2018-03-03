using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using Security.Contracts;
using Security.Contracts.JWT;
using System;
using System.Linq;

namespace Queries.RequestHandlers {
  public class LoginUserHandler : IRequestHandler<LoginUser, Tuple<bool, string, string>> {
    private IRead<UserDirectory> _userDirectoryReader;
    private PasswordService _passwordService;
    private ITokenGenerator _tokenGenerator;
    private IJWTService _jwtService;

    public LoginUserHandler(IRead<UserDirectory> userDirectoryReader, ITokenGenerator tokenGenerator, IJWTService jwtService, PasswordService passwordService) {
      _userDirectoryReader = userDirectoryReader;
      _passwordService = passwordService;
      _tokenGenerator = tokenGenerator;
      _jwtService = jwtService;
    }

    public Tuple<bool, string, string> Handle(LoginUser request) {
      var user = _userDirectoryReader.Where(ud => ud.Username.Equals(request.Username, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();

      if (user != null && _passwordService.IsPasswordValid(request.Password, user.Password, out string updatedPasswordHash)) {
        var encodedToken = _jwtService.EncodeToken(_tokenGenerator.CreateFromUserId(user.UserId));
        return new Tuple<bool, string, string>(true, encodedToken, updatedPasswordHash);
      }

      return new Tuple<bool, string, string>(false, String.Empty, String.Empty);
    }
  }
}
