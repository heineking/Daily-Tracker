using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public class UpdateQuestion : IEvent {
    public int QuestionId { get; set; }
    public int QuestionnaireId { get; set; }
    public string Text { get; set; }
    public int SavedById { get; set; }
  }
}
