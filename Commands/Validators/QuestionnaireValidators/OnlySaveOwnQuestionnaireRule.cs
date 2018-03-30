using Commands.Events;
using Commands.Validators._Rules;
using Mediator.Contracts;

namespace Commands.Validators {
  public class OnlySaveOwnQuestionnaireRule : OwnsQuestionnaireRule<UpdateQuestionnaire> {

    public OnlySaveOwnQuestionnaireRule(IHub hub) : base(hub) {
    }

    public override int GetUserId(UpdateQuestionnaire entity) {
      return entity.SavedById;
    }

    public override int GetQuestionnaireId(UpdateQuestionnaire entity) {
      return entity.QuestionnaireId;
    }
  }
}
