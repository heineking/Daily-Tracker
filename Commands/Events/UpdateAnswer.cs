using Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Mediator.Contracts;
using Commands.Mapping;
using DataAccessLayer.Contracts.Entities;

namespace Commands.Events {

  public interface IUpdateAnswer {
    [Mapping(nameof(Answer.OptionId))]
    int OptionId { get; set; }

    [Mapping(nameof(Answer.QuestionId))]
    int QuestionId { get; set; }
  }

  public class UpdateAnswer : IUpdateAnswer, IEvent, IUpdateEvent {
    public int AnswerId { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public int UserId { get; set; }
    public List<PropertyInfo> DirtyProperties { get; set; }

    public UpdateAnswer() {
      DirtyProperties = new List<PropertyInfo>();
    }
  }
}
