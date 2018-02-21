using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Events {
  public abstract class SaveQuestion {
    public virtual int QuestionId { get; set; }
    public int QuestionnaireId { get; set; }
    public string Text { get; set; }
  }

  public class CreateQuestion : SaveQuestion, IEvent {

    public override int QuestionId { get => base.QuestionId; set { /* no-op */ } }

    public void SetQuestionId(int questionId) {
      base.QuestionId = questionId;
    }

  }

  public class UpdateQuestion : SaveQuestion, IEvent {

  }

}
