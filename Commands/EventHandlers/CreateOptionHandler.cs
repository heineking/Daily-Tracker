using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;

namespace Commands.EventHandlers {
  public class CreateOptionHandler : IEventHandler<CreateOption> {
    private readonly IHub _hub;
    private readonly ISave<Option> _optionSaver;

    public CreateOptionHandler(IHub hub, ISave<Option> optionSaver) {
      _hub = hub;
      _optionSaver = optionSaver;
    }

    public void Handle(CreateOption @event) {
      // TODO: use automapper ?
      var option = new Option {
        OptionText = @event.Text,
        OptionValue = @event.Value,
        QuestionId = @event.QuestionId
      };

      _optionSaver.Save(option);
      _hub.Publish(new Commit());

      @event.OptionId = option.OptionId;
    }
  }
}
