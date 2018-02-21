using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public class SaveQuestion : IEvent {
    public bool ShouldCreate => QuestionId == default(int);
    public int QuestionId { get; set; }
    public int QuestionnaireId { get; set; }
    public string Text { get; set; }
  }
}
