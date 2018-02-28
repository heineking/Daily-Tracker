using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Security.Contracts.Hashing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.EventHandlers {
  public class CreateUserHandler : IEventHandler<CreateUser> {
    private readonly ISave<User> _userSaver;
    private readonly IHub _hub;
    private readonly IHasher _hasher;

    public CreateUserHandler(ISave<User> userSaver, IHasher hasher, IHub hub) {
      _userSaver = userSaver;
      _hub = hub;
      _hasher = hasher;
    }

    public void Handle(CreateUser @event) {
      var user = new User {
        BirthDate = @event.BirthDate,
        Email = @event.Email,
        FirstName = @event.FirstName,
        LastName = @event.LastName,
        PhoneNumber = @event.PhoneNumber,
        UserDirectory = new UserDirectory {
          Password = _hasher.CreatePassword(@event.Password).ToString(),
          Username = @event.Username
        }
      };
      _userSaver.Save(user);
      _hub.Publish(new Commit());
      @event.SetUserId(user.UserId);
    }
  }
}
