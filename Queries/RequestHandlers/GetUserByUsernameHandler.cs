using AutoMapper;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Models;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queries.RequestHandlers {
  public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsername, UserModel> {
    private readonly IRead<User> _userReader;
    private readonly IMapper _mapper;

    public GetUserByUsernameHandler(IRead<User> userReader, IMapper mapper) {
      _userReader = userReader;
      _mapper = mapper;
    }

    public UserModel Handle(GetUserByUsername request) {
      var user = _userReader
        .Where(u => u.UserDirectory.Username.Equals(request.Username, StringComparison.InvariantCultureIgnoreCase))
        .Single();

      return user != null ? _mapper.Map<UserModel>(user) : null;
    }
  }
}
