using Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Contracts {
  public interface IValidatable<T> where T : class {
    bool Validate(IValidator<T> validator, out IEnumerable<Error> brokenRules);
  }
}
