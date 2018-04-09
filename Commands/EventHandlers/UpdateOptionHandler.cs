using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.EventHandlers {
  public class UpdateOptionHandler : IEventHandler<UpdateOption> {
    private readonly IHub _hub;
    private readonly ISave<Option> _optionSaver;

    public UpdateOptionHandler(IHub hub, ISave<Option> optionSaver) {
      _hub = hub;
      _optionSaver = optionSaver;
    }

    public void Handle(UpdateOption @event) {
      var option = new Option {
         OptionText = @event.Text,
         OptionId = @event.OptionId,
         OptionValue = @event.Value,
         QuestionId = @event.QuestionId
      };

      _optionSaver.Save(option);
      _hub.Publish(new Commit());
      
    }
  }
}
