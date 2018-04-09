using Commands.Events;
using Commands.ValidationHandlers;
using Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators.OptionValidators {
  public class CreateOptionValidator : AbstractValidator<CreateOption> {
    public CreateOptionValidator(SingleInstanceFactory singleInstanceFactory) {
      RuleFor(create => create.OptionId)
        .MustBeDefault("Option Id must be unset.");

      RuleFor(create => create.QuestionId)
        .Required("Question Id is required.");

      RuleFor(create => create.SavedById)
        .Required("Saved By Id is required.")
        .MustBe(singleInstanceFactory(typeof(CreateOptionOnOwnQuestionnaireRule)));

      RuleFor(create => create.Text)
        .NotEmpty("Text must not be empty")
        .NotNull("Text must not be null")
        .MaxLength(100, "Text must be less than 100 characters");
    }
  }
}
