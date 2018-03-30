using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Commands.Events;

namespace Commands.EventHandlers {
  public class DeleteQuestionnaireHandler : IEventHandler<DeleteQuestionnaire> {
    private readonly IDelete<Questionnaire> _questionnaireDelete;
    private readonly IHub _hub;
    
    public DeleteQuestionnaireHandler(IDelete<Questionnaire> questionnaireDelete, IHub hub) {
      _questionnaireDelete = questionnaireDelete;
      _hub = hub;
    }

    public void Handle(DeleteQuestionnaire @event) {
      _questionnaireDelete.DeleteWhere(q => q.QuestionnaireId == @event.QuestionnaireId);
      _hub.Publish(new Commit());
    }
  }
}
