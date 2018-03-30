using Commands.Events;
using Commands.ValidationHandlers;
using Commands.Validators._Rules;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators
{
  public class CanOnlySaveQuestionsOnOwnQuestionnaire : OwnsQuestionnaireRule<CreateQuestion> {

    public CanOnlySaveQuestionsOnOwnQuestionnaire(IHub hub) : base(hub) {
    }

    public override int GetUserId(CreateQuestion entity) {
      return entity.SavedById;
    }

    public override int GetQuestionnaireId(CreateQuestion entity) {
      return entity.QuestionnaireId;
    }
  }
}
