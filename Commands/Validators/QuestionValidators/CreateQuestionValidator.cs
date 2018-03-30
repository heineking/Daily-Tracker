using Commands.Events;
using Commands.ValidationHandlers;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators {

  //public class CreateQuestionValidator : AbstractValidator<CreateQuestion> {
  //  public CreateQuestionValidator(SingleInstanceFactory singleFactory) {

  //    RuleFor(create => create.QuestionnaireId)
  //      .Required("Questionnaire Id is required");

  //    RuleFor(create => create.SavedById)
  //      .Required("Saved By Id is required")
  //      .MustBe((CanOnlySaveQuestionsOnOwnQuestionnaire)singleFactory(typeof(CanOnlySaveQuestionsOnOwnQuestionnaire)));

  //    RuleFor(create => create.Text)
  //      .NotNull("Text cannot be null")
  //      .NotEmpty("Text cannot by empty")
  //      .MaxLength(200, "Text cannot be longer than 200 characters");

  //  }
  //}
}
