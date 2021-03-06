﻿using Commands.Events;
using Commands.ValidationHandlers;
using Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Validators.OptionValidators {
  public class UpdateOptionValidator : AbstractValidator<UpdateOption> {
    public UpdateOptionValidator(SingleInstanceFactory singleInstanceFactory) {
      RuleFor(update => update.OptionId)
        .Required("Option Id is required");

      RuleFor(update => update.SavedById)
        .Required("Saved By Id is required")
        .MustBe(singleInstanceFactory(typeof(UpdateOptionOnOwnQuestionnaireRule)));
      
    }
  }
}
