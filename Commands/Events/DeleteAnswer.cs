using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public class DeleteAnswer : IEvent {
    public int AnswerId { get; set; }
    public int DeletedById { get; set; }
  }
}
