using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using Security.Contracts;
using Security.Contracts.Hashing;
using System.Linq;

namespace Queries.RequestHandlers {
  public class ValidateLoginHandler : IRequestHandler<ValidateLogin, bool> {
    private readonly IRead<UserDirectory> _userDirectoryReader;
    private readonly ISave<UserDirectory> _userDirectorySaver;
    private readonly PasswordService _passwordService;
    private readonly IHub _hub;

    public ValidateLoginHandler(IRead<UserDirectory> userDirectoryReader, ISave<UserDirectory> userDirectorySaver, PasswordService passwordService, IHub hub) {
      _passwordService = passwordService;
      _userDirectoryReader = userDirectoryReader;
      _userDirectorySaver = userDirectorySaver;
      _hub = hub;
    }
    public bool Handle(ValidateLogin request) {
      var user = _userDirectoryReader
        .Where(ud => ud.Username == request.Username)
        .ToList()
        .Single();

      var password = new Password(user.Password);
      var updated = string.Empty;
      if (_passwordService.IsPasswordValid(request.Password, password, out updated)) {
        user.Password = updated;
        _hub.Publish(new Commit());
        return true;
      }
      return false;
    }
  }
}
