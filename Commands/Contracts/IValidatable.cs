using Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Contracts {
  public interface IValidatable<T> {
    IEnumerable<Error> Validate(IValidator<T> validator);
  }
}
