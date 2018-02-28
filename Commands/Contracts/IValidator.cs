using Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Contracts {
  public interface IValidator<T> {
    IEnumerable<Error> Errors(T entity);
  }
}
