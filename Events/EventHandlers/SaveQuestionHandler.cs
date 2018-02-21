using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.EventHandlers {
  public class SaveQuestionHandler : IEventHandler<SaveQuestion> {
    private readonly IRead<Question> _questionReader;
    private readonly ISave<Question> _questionSaver;

    public SaveQuestionHandler(IRead<Question> questionReader, ISave<Question> questionSaver) {
      _questionReader = questionReader;
      _questionSaver = questionSaver;
    }

    public void Handle(SaveQuestion @event) {
      var question = @event.ShouldCreate
        ? new Question { }
        : _questionReader.GetById(@event.QuestionId);
      question.QuestionText = @event.Text;
      question.QuestionnaireId = @event.QuestionnaireId;

      _questionSaver.Save(question);
      @event.QuestionId = question.QuestionId;
    }
  }
}
