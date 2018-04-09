using Commands.Events;
using Commands.ValidationHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators
{

  public class CreateQuestionnaireValidator : AbstractValidator<CreateQuestionnaire> {
    public CreateQuestionnaireValidator() {

      RuleFor(createQuestionnaire => createQuestionnaire.QuestionnaireId)
        .MustBe((questionnaireId) => (int)questionnaireId == default(int), "Questionnaire Id must be unset");

      RuleFor(createQuestionnaire => createQuestionnaire.Name)
        .NotNull("Name cannot be null")
        .NotEmpty("Name cannot be empty")
        .MaxLength(100, "Name cannot be longer than 100 characters");

      RuleFor(createQuestionnaire => createQuestionnaire.Description)
        .MaxLength(200, "Description cannot be longer than 200 characters");

      RuleFor(createQuestionnaire => createQuestionnaire.SavedById)
        .Required("User Id is Required");
    }
  }
}
