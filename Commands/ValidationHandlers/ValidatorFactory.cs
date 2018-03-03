using Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.ValidationHandlers {
  public class ValidatorFactory {
    private readonly SingleInstanceFactory _singleInstanceFactory;
    public ValidatorFactory(SingleInstanceFactory singleInstanceFactory) {
      _singleInstanceFactory = singleInstanceFactory;
    }
    public AbstractValidator<TEntity> CreateValidator<TEntity>() where TEntity : class {
      return (AbstractValidator<TEntity>)_singleInstanceFactory(typeof(AbstractValidator<TEntity>));
    }
  }
}
