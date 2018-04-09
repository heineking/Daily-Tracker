using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.ValidationHandlers {
  public interface IRule<T> {
    string Message { get; set; }
    Func<T, bool> IsValid { get; set; }
  }
  public class Rule<T> : IRule<T> {
    public string Message { get; set; }
    public Func<T, bool> IsValid { get; set; }
  }
}
