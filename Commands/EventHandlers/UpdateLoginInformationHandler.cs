using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.EventHandlers {
  public class UpdateLoginInformationHandler : IEventHandler<UpdateLoginInformation> {
    private readonly ISave<UserDirectory> _userDirectorySaver;
    private readonly IRead<UserDirectory> _userDirectoryReader;
    private readonly IHub _hub;

    public UpdateLoginInformationHandler(ISave<UserDirectory> userDirectorySaver, IRead<UserDirectory> userDirectoryReader, IHub hub) {
      _userDirectorySaver = userDirectorySaver;
      _userDirectoryReader = userDirectoryReader;
      _hub = hub;
    }
    public void Handle(UpdateLoginInformation @event) {
      var userDirectory = _userDirectoryReader.Where(ud => ud.Username.Equals(@event.Username, StringComparison.InvariantCultureIgnoreCase)).Single();
      userDirectory.Password = @event.Password;

      _hub.Publish(new Commit());
    }
  }
}
