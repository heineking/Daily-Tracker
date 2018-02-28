using Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Contracts {
  public interface IValidator<T> {
    bool IsValid(T entity);
    IEnumerable<Error> BrokenRules(T entity);
  }
}
