using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Errors {
  public class Error {
    public string Property { get; set; }
    public string Message { get; set; }
    public Error(string property, string message) {
      Property = Char.ToLowerInvariant(property[0]) + property.Substring(1);
      Message = message;
    }
  }
}
