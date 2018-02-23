using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.EventHandlers {
  public class UpdateQuestionHandler : IEventHandler<UpdateQuestion> {
    private readonly ISave<Question> _questionSaver;
    private readonly IRead<Question> _questionReader;
    private readonly IHub _hub;

    public UpdateQuestionHandler(ISave<Question> questionSaver, IRead<Question> questionReader, IHub hub) {
      _questionReader = questionReader;
      _questionSaver = questionSaver;
      _hub = hub;
    }

    public void Handle(UpdateQuestion @event) {
      var question = _questionReader.GetById(@event.QuestionId);
      question.QuestionText = @event.Text;
      question.QuestionnaireId = @event.QuestionnaireId;
      _questionSaver.Save(question);
      _hub.Publish(new Commit());
    }

  }
}
