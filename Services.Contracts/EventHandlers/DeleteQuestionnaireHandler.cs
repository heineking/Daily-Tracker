using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using Services.Contracts.Events;

namespace Services.Contracts.EventHandlers {
  public class DeleteQuestionnaireHandler : IEventHandler<DeleteQuestionnaire> {
    private readonly IDelete<Questionnaire> _questionnaireDelete;
    
    public DeleteQuestionnaireHandler(IDelete<Questionnaire> questionnaireDelete) {
      _questionnaireDelete = questionnaireDelete;
    }

    public void Handle(DeleteQuestionnaire @event) {
      _questionnaireDelete.DeleteWhere(q => q.QuestionnaireId == @event.QuestionnaireId);
    }
  }
}
