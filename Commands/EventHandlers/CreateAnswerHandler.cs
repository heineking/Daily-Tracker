using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using System;

namespace Commands.EventHandlers {
  public class CreateAnswerHandler : IEventHandler<CreateAnswer> {
    private readonly IHub _hub;
    private readonly ISave<Answer> _answerSaver;
    private readonly IRead<Option> _optionReader;

    public CreateAnswerHandler(IHub hub, IRead<Option> optionReader, ISave<Answer> answerSaver) {
      _hub = hub;
      _answerSaver = answerSaver;
    }

    public void Handle(CreateAnswer @event) {

      var option = _hub.Send(new GetOptionById(@event.OptionId));

      var answer = new Answer {
        AnswerDate = DateTime.UtcNow,
        OptionId = @event.OptionId,
        QuestionId = option.QuestionId,
        UserId = @event.UserId
      };

      _answerSaver.Save(answer);
      _hub.Publish(new Commit());

      @event.AnswerId = answer.AnswerId;
    }
  }
}
