using Commands.Events;
using Commands.Validators._Rules;
using Mediator.Contracts;

namespace Commands.Validators.QuestionnaireValidators
{
  public class DeleteOwnQuestionnaireRule : OwnsQuestionnaireRule<DeleteQuestionnaire> {

    public DeleteOwnQuestionnaireRule(IHub hub) : base(hub) {
    }

    public override int GetUserId(DeleteQuestionnaire entity) {
      return entity.DeletedById;
    }

    public override int GetQuestionnaireId(DeleteQuestionnaire entity) {
      return entity.QuestionnaireId;
    }
  }
}
