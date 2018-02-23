using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Commands.Events;
using System;

namespace Commands.EventHandlers {
  public class UpdateQuestionnaireHandler : IEventHandler<UpdateQuestionnaire> {
    private readonly ISave<Questionnaire> _questionnaireSaver;
    private readonly IRead<Questionnaire> _questionnaireReader;
    private readonly IHub _hub;

    public UpdateQuestionnaireHandler(IRead<Questionnaire> questionnaireReader, ISave<Questionnaire> questionnaireSaver, IHub hub) {
      _questionnaireReader = questionnaireReader;
      _questionnaireSaver = questionnaireSaver;
      _hub = hub;
    }

    public void Handle(UpdateQuestionnaire @event) {
      var questionnaire = _questionnaireReader.GetById(@event.QuestionnaireId);

      questionnaire.Name = @event.Name;
      questionnaire.Description = @event.Description;
      questionnaire.Public = @event.Public;
      _questionnaireSaver.Save(questionnaire);

      _hub.Publish(new Commit());
    }
  }
}
