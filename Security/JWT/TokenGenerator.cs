using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Security.Contracts.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Security.JWT {
  public class TokenGenerator : ITokenGenerator {

    private readonly ITokenSettings _settings;
    private readonly IRead<UserDirectory> _userDirectoryReader;

    public TokenGenerator(ITokenSettings settings, IRead<UserDirectory> userDirectoryReader) {
      _settings = settings;
      _userDirectoryReader = userDirectoryReader;
    }

    public Token CreateFromUserId(int userId) {
      // TODO: Refactor out into RequestHandler / Request
      var user = _userDirectoryReader.Where(ud => ud.UserId == userId).Single();
      return new Token {
        UserId = user.UserId,
        Username = user.Username,
        Exp = DateTime.Now.AddHours(_settings.ExpInHours),
        Iss = _settings.Issuer
      };
    }
  }
}
