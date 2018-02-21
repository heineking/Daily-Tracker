using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Commands.Events;
using System;

namespace Commands.EventHandlers {
  public class SaveQuestionnaireHandler : IEventHandler<SaveQuestionnaire> {
    private readonly ISave<Questionnaire> _questionnaireSaver;
    private readonly IRead<Questionnaire> _questionnaireReader;

    public SaveQuestionnaireHandler(IRead<Questionnaire> questionnaireReader, ISave<Questionnaire> questionnaireSaver) {
      _questionnaireReader = questionnaireReader;
      _questionnaireSaver = questionnaireSaver;
    }

    public void Handle(SaveQuestionnaire @event) {
      var questionnaire = @event.ShouldCreate
        ? new Questionnaire { CreatedDate = DateTime.Now }
        : _questionnaireReader.GetById(@event.QuestionnaireId);

      questionnaire.Name = @event.Name;
      questionnaire.Description = @event.Description;
      _questionnaireSaver.Save(questionnaire);

      @event.QuestionnaireId = questionnaire.QuestionnaireId;
    }
  }
}
