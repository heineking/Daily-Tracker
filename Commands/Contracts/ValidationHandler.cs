using Infrastructure.Errors;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.Contracts {
  public interface IValidatorHandler {
   Tuple<bool, IEnumerable<Error>> IsValid<TValidator, TEntity>(TEntity entity) where TValidator : IValidator<TEntity> where TEntity : IValidatable<TEntity>;
  }
  public class ValidatorHandler : IValidatorHandler {
    private readonly SingleInstanceFactory _singleInstanceFactory;

    public ValidatorHandler(SingleInstanceFactory singleFactory) {
      _singleInstanceFactory = singleFactory;
    }

    public Tuple<bool, IEnumerable<Error>> IsValid<TValidator, TEntity>(TEntity entity) where TValidator : IValidator<TEntity> where TEntity : IValidatable<TEntity> {
      var validator = _singleInstanceFactory(typeof(TValidator)) as IValidator<TEntity>;
      var errors = validator.BrokenRules(entity).ToList();
      return new Tuple<bool, IEnumerable<Error>>(errors.Count == 0, errors);
    }
  }
}
