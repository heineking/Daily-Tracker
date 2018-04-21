using Commands.Events;
using Commands.Mapping;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.EventHandlers {
  public class UpdateOptionHandler : IEventHandler<UpdateOption> {
    private readonly IHub _hub;
    private readonly ISave<Option> _optionSaver;
    private readonly IRead<Option> _optionReader;

    public UpdateOptionHandler(IHub hub, ISave<Option> optionSaver, IRead<Option> optionReader) {
      _hub = hub;
      _optionSaver = optionSaver;
      _optionReader = optionReader;
    }

    public void Handle(UpdateOption @event) {

      var option = _optionReader.GetById(@event.OptionId);

      @event.ApplyPropertyUpdates(option);

      _optionSaver.Save(option);
      _hub.Publish(new Commit());

    }
  }
}
