using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.EventHandlers {
  public class CreateQuestionnaireHandler : IEventHandler<CreateQuestionnaire> {
    private readonly ISave<Questionnaire> _questionnaireSaver;
    private readonly IHub _hub;

    public CreateQuestionnaireHandler(ISave<Questionnaire> questionnaireSaver, IHub hub) {
      _questionnaireSaver = questionnaireSaver;
      _hub = hub;
    }

    public void Handle(CreateQuestionnaire @event) {
      var questionnaire = new Questionnaire {
        CreatedById = @event.UserId,
        CreatedDate = DateTime.Now,
        Description = @event.Description,
        Name = @event.Name,
        Public = @event.Public
      };

      _questionnaireSaver.Save(questionnaire);

      _hub.Publish(new Commit());

      @event.SetQuestionnaireId(questionnaire.QuestionnaireId);
    }
  }
}
