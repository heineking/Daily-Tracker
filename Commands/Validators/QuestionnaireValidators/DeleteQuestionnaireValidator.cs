using Commands.Events;
using Commands.ValidationHandlers;
using Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators.QuestionnaireValidators {
  public class DeleteQuestionnaireValidator : AbstractValidator<DeleteQuestionnaire> {
    public DeleteQuestionnaireValidator(SingleInstanceFactory singleFactory) {

      RuleFor(request => request.DeletedByUserId)
        .Required("User Id is required")
        .MustBe((DeleteOwnQuestionnaireRule)singleFactory(typeof(DeleteOwnQuestionnaireRule)));

      RuleFor(request => request.QuestionnaireId)
        .Required("Questionnaire Id is required")
        .MustBe((QuestionnaireExistsRule)singleFactory(typeof(QuestionnaireExistsRule)));
    }
  }
}
