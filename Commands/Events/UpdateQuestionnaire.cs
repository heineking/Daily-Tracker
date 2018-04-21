using Commands.Contracts;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Errors;
using DataAccessLayer.Contracts.Entities;
using Commands.Mapping;
using System.Reflection;

namespace Commands.Events {
  public interface IUpdateQuestionnaire {
    [Mapping(nameof(Questionnaire.Name))]
    string Name { get; set; }

    [Mapping(nameof(Questionnaire.Description))]
    string Description { get; set; }

    [Mapping(nameof(Questionnaire.Public))]
    bool Public { get; set; }
  }

  public class UpdateQuestionnaire : IEvent, IUpdateQuestionnaire, IUpdateEvent {
    public int QuestionnaireId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Public { get; set; }
    public int SavedById { get; set; }
    public List<PropertyInfo> DirtyProperties { get; set; }

    public UpdateQuestionnaire() {
      DirtyProperties = new List<PropertyInfo>();
    }
  }
}
