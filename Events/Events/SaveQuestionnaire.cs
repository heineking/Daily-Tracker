using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public class SaveQuestionnaire : IEvent {
    public bool ShouldCreate => QuestionnaireId == default(int);
    public int QuestionnaireId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}
