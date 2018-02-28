﻿using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Requests {
  public class ValidateLogin : IRequest<Tuple<bool, string>> {
    public string Username { get; set; }
    public string Password { get; set; }
  }
}
