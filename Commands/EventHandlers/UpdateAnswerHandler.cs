using Commands.Events;
using Commands.Mapping;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Queries.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.EventHandlers
{
  public class UpdateAnswerHandler : IEventHandler<UpdateAnswer> {
    private readonly IHub _hub;
    private readonly ISave<Answer> _answerSaver;
    private readonly IRead<Answer> _answerReader;

    public UpdateAnswerHandler(IHub hub, ISave<Answer> answerSaver, IRead<Answer> answerReader) {
      _hub = hub;
      _answerReader = answerReader;
      _answerSaver = answerSaver;
    }

    public void Handle(UpdateAnswer @event) {
      var option = _hub.Send(new GetOptionById(@event.OptionId));

      if (option.QuestionId != @event.QuestionId) {
        @event.QuestionId = option.QuestionId;
        @event.DirtyProperties.Add(typeof(IUpdateAnswer).GetProperty(nameof(IUpdateAnswer.QuestionId)));
      }

      var entity = _answerReader.GetById(@event.AnswerId);

      @event.ApplyPropertyUpdates(entity);

      _answerSaver.Save(entity);

      _hub.Publish(new Commit());
    }
  }
}
