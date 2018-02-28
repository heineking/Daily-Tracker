using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public class UpdateLoginInformation : IEvent {
    public string Username { get; set; }
    public string Password { get; set; }
  }
}
