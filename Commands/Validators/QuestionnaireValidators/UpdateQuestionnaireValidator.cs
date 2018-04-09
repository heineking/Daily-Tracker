using Commands.Events;
using Commands.ValidationHandlers;
using Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators {
  public class UpdateQuestionnaireValidator : AbstractValidator<UpdateQuestionnaire> {
    public UpdateQuestionnaireValidator(SingleInstanceFactory singleFactory) {

      RuleFor(update => update.QuestionnaireId)
        .Required("Questionnaire Id is required");

      RuleFor(createQuestionnaire => createQuestionnaire.Name)
        .NotNull("Name cannot be null")
        .NotEmpty("Name cannot be empty")
        .MaxLength(100, "Name cannot be longer than 100 characters");

      RuleFor(createQuestionnaire => createQuestionnaire.Description)
        .MaxLength(200, "Description cannot be longer than 200 characters");

      RuleFor(update => update.SavedById)
        .Required("User Id is Required")
        .MustBe(singleFactory(typeof(OnlySaveOwnQuestionnaireRule)));
    }
  }
}
