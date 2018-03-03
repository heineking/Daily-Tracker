﻿using Commands.Contracts;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Errors;

namespace Commands.Events {
  public abstract class SaveQuestionnaire {
    public virtual int QuestionnaireId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Public { get; set; }
    public int SavedById { get; protected set; }
    public void SetSavedById(int id) {
      SavedById = id;
    }
  }

  public class UpdateQuestionnaire : SaveQuestionnaire, IEvent {
  }

  public class CreateQuestionnaire : SaveQuestionnaire, IEvent {

    public override int QuestionnaireId { get => base.QuestionnaireId; set { /* no-op */ } }

    public void SetQuestionnaireId(int questionnaireId) {
      base.QuestionnaireId = questionnaireId;
    }
  }
}
