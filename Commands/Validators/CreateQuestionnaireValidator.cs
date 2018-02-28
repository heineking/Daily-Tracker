using Commands.Contracts;
using Commands.Events;
using Infrastructure.Errors;
using System.Collections.Generic;

namespace Commands.Validators {
  public class CreateQuestionnaireValidator : IValidator<CreateQuestionnaire> {
    public IEnumerable<Error> Errors(CreateQuestionnaire entity) {
      if (entity.UserId == default(int))
        yield return new Error(nameof(entity.UserId), "User Id is required");

      if (string.IsNullOrEmpty(entity.Name)) {
        yield return new Error(nameof(entity.Name), "Name is required");
      } else {
        if (!(entity.Name.Length > 1 && entity.Name.Length < 100))
          yield return new Error(nameof(entity.Name), "Name must be between 1 and 100 characters long");
      }

      if (!string.IsNullOrEmpty(entity.Description) && entity.Description.Length > 200)
        yield return new Error(nameof(entity.Description), "Description cannot be greater that 200 characters long");

    }
  }
}
