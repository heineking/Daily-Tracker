using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.EventHandlers {
  public class CreateQuestionHandler : IEventHandler<CreateQuestion> {
    private readonly ISave<Question> _questionSaver;
    private readonly IHub _hub;

    public CreateQuestionHandler(ISave<Question> questionSaver, IHub hub) {
      _questionSaver = questionSaver;
      _hub = hub;
    }

    public void Handle(CreateQuestion @event) {
      var question = new Question {
        QuestionText = @event.Text,
        QuestionnaireId = @event.QuestionnaireId
      };

      _questionSaver.Save(question);
      _hub.Publish(new Commit());
      @event.SetQuestionId(question.QuestionId);
    }
  }
}
