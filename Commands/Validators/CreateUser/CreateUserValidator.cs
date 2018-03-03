using Commands.Events;
using Commands.ValidationHandlers;
using Mediator;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Commands.Validators {

  public class CreateUserValidator : AbstractValidator<CreateUser> {
    public CreateUserValidator(SingleInstanceFactory singleFactory) {

      RuleFor(createUser => createUser.Username)
        .NotNull("Username cannot be null")
        .NotEmpty("Username cannot be empty")
        .MaxLength(50, "Username cannot be longer than 50 characters")
        .MustBe((UniqueUsernameRule)singleFactory(typeof(UniqueUsernameRule)));

      RuleFor(createUser => createUser.FirstName)
        .NotNull("First Name cannot be null")
        .NotEmpty("First Name cannot be empty")
        .MatchesPattern(@"^[a-zA-Z]+$", "First Name can only contain letters")
        .MaxLength(50, "First Name cannot be longer than 50 characters");

      RuleFor(createUser => createUser.LastName)
        .NotNull("Last Name cannot be null")
        .NotEmpty("Last Name cannot be empty")
        .MatchesPattern(@"^[a-zA-Z]+$", "Last Name can only contain letters")
        .MaxLength(100, "Last Name cannot be longer than 100 characters");

      RuleFor(createUser => createUser.Email)
        .NotNull("Email cannot be null")
        .NotEmpty("Email cannot be empty")
        .MaxLength(100, "Email cannot be longer than 100 characters")
        .MustBe(value => ValidEmailFormat((string)value), "Email is invalid format");

      RuleFor(createUser => createUser.Password)
        .NotNull("Password cannot be null")
        .NotEmpty("Password cannot be empty")
        .MatchesPattern(
          @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}",
          "Password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character"
        );

      RuleFor(createUser => createUser.BirthDate)
        .NotNull("Birth Date cannot be null")
        .MinDate(DateTime.Now.AddDays(-1).AddYears(-18), "Must be at least 18 years old to register");

    }

    public static bool ValidEmailFormat(string address) {
      try {
        new MailAddress(address);
        return true;
      } catch (FormatException) {
        return false;
      }
    }
  }
}
