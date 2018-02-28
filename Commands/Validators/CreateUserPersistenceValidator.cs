using Commands.Contracts;
using Commands.Events;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Commands.Validators {
  public class UniqueUsernameValidator : IValidator<CreateUser> {
    private readonly IRead<UserDirectory> _userDirectoryReader;

    public UniqueUsernameValidator(IRead<UserDirectory> userDirectoryReader) {
      _userDirectoryReader = userDirectoryReader;
    }

    public IEnumerable<Error> BrokenRules(CreateUser entity) {
      var usernameExists = _userDirectoryReader.Where(ud => ud.Username == entity.Username).SingleOrDefault();
      if (usernameExists != null)
        yield return new Error(nameof(entity.Username), "Username already exists");
      yield break;
    }

    public bool IsValid(CreateUser entity) {
      return BrokenRules(entity).Count() == 0;
    }
  }

  public class CreateUserValidator : IValidator<CreateUser> {

    private bool IsEmailValid(string email) {
      try {
        var m = new MailAddress(email);
        return true;
      } catch (FormatException) {
        return false;
      }
    }

    private readonly IValidatorHandler _validatorHandler;

    public CreateUserValidator(IValidatorHandler validatorHandler) {
      _validatorHandler = validatorHandler;
    }

    public IEnumerable<Error> BrokenRules(CreateUser entity) {
      if (string.IsNullOrEmpty(entity.Username)) {
        yield return new Error(nameof(entity.Username), "Username is required");
      } else {
        var (isValid, errors) = _validatorHandler.IsValid<UniqueUsernameValidator, CreateUser>(entity);
        foreach (var error in errors) {
          yield return error;
        }
      }

      if (string.IsNullOrEmpty(entity.FirstName))
        yield return new Error(nameof(entity.FirstName), "First Name is required");

      if (string.IsNullOrEmpty(entity.LastName))
        yield return new Error(nameof(entity.LastName), "Last Name is required");


      if (!IsEmailValid(entity.Email))
        yield return new Error(nameof(entity.Email), "Email address is invalid");


      yield break;
    }

    public bool IsValid(CreateUser entity) {
      return BrokenRules(entity).Count() == 0;
    }
  }
}
