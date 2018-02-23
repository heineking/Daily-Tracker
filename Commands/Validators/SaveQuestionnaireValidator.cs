using Commands.Events;
using FluentValidation;
namespace Commands.Validators {
  public class SaveQuestionnaireValidator : AbstractValidator<SaveQuestionnaire> {
    public SaveQuestionnaireValidator() {
      RuleFor(req => req.Name).NotEmpty().WithMessage("Name must be provided");
      RuleFor(req => req.Name).MaximumLength(100).WithMessage("Name cannot be more than 100 characters long");
      RuleFor(req => req.Description).MaximumLength(200).WithMessage("Description cannot be more than 200 characters long");
    }
  }
}
