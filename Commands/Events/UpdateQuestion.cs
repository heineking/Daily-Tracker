using Commands.Contracts;
using Commands.Mapping;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Commands.Events {

  public interface IUpdateQuestion {

    [Mapping(nameof(Question.QuestionnaireId))]
    int QuestionnaireId { get; set; }

    [Mapping(nameof(Question.QuestionText))]
    string Text { get; set; }
  }

  public class UpdateQuestion : IEvent, IUpdateQuestion, IUpdateEvent {
    public int QuestionId { get; set; }
    public int QuestionnaireId { get; set; }
    public string Text { get; set; }
    public int SavedById { get; set; }
    public List<PropertyInfo> DirtyProperties { get; set; }

    public UpdateQuestion() {
      DirtyProperties = new List<PropertyInfo>();
    }
  }
}
