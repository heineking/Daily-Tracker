using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Commands.Events;
using System;
using Commands.Mapping;

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
      var entity = _questionnaireReader.GetById(@event.QuestionnaireId);
      @event.ApplyPropertyUpdates(entity);

      _questionnaireSaver.Save(entity);

      _hub.Publish(new Commit());
    }
  }
}
