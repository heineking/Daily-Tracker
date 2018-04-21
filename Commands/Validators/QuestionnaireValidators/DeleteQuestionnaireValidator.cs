using Commands.Events;
using Commands.ValidationHandlers;
using Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators.QuestionnaireValidators {
  public class DeleteQuestionnaireValidator : AbstractValidator<DeleteQuestionnaire> {
    public DeleteQuestionnaireValidator(SingleInstanceFactory singleFactory) {

      RuleFor(request => request.DeletedById)
        .Required("Delete by Id is required")
        .MustBe(singleFactory(typeof(DeleteOwnQuestionnaireRule)));

      RuleFor(request => request.QuestionnaireId)
        .Required("Questionnaire Id is required")
        .MustBe(singleFactory(typeof(QuestionnaireExistsRule)));
    }
  }
}
