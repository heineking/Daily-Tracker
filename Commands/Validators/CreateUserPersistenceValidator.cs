using Commands.Contracts;
using Commands.Events;
using Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Commands.Validators
{
  public class CreateUserPersistenceValidator : IValidator<CreateUser> {

    private IEnumerable<Error> RequiredStringProperties(CreateUser user, params string[] propertyNames) {
      var propertyInfos = typeof(CreateUser).GetProperties();
      foreach(var propName in propertyNames) {
        var propertyInfo = propertyInfos.Single(p => p.Name == propName);
        var propertyValue = Convert.ToString(propertyInfo.GetValue(user, null));
        if (string.IsNullOrEmpty(propertyValue)) {
          yield return new Error(propName, $"{propName} cannot be null or empty");
        }
      }
      yield break;
    }

    private bool IsEmailValid(string email) {
      try {
        var m = new MailAddress(email);
        return true;
      } catch (FormatException) {
        return false;
      }
    }

    public IEnumerable<Error> BrokenRules(CreateUser entity) {
      
      var errorMessages = RequiredStringProperties(entity,
        nameof(entity.FirstName),
        nameof(entity.LastName),
        nameof(entity.BirthDate),
        nameof(entity.Username),
        nameof(entity.Password));

      foreach (var errorMessage in errorMessages) {
        yield return errorMessage;
      }

      if (!IsEmailValid(entity.Email))
        yield return new Error(nameof(entity.Email), "Email address is invalid");
      

      yield break;
    }

    public bool IsValid(CreateUser entity) {
      return BrokenRules(entity).Count() == 0;
    }
  }
}
