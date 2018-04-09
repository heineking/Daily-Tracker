using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public class DeleteOption : IEvent {
    public int OptionId { get; set; }
    public int DeletedById { get; set; }
  }
}
