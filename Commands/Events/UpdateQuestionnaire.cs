using Commands.Contracts;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Errors;

namespace Commands.Events {
  public class UpdateQuestionnaire : IEvent {
    public int QuestionnaireId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Public { get; set; }
    public int SavedById { get; set; }
  }
}
