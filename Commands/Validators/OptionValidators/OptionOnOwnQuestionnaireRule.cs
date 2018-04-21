using Commands.Events;
using Commands.ValidationHandlers;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using Mediator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.Validators.OptionValidators {
  public abstract class OptionOnOwnQuestionnaireRule<T> : IRule<T> where T : SaveOption {
    private IRead<Questionnaire> _questionnaireReader;

    public string Message { get; set; }
    public Func<T, bool> IsValid { get { return Validate; } set { } }

    public OptionOnOwnQuestionnaireRule(IRead<Questionnaire> questionnaireReader) {
      _questionnaireReader = questionnaireReader;
    }

    private bool Validate(T option) {
      var optionId = GetOptionId(option);
      var userId = GetUserId(option);
      var questionnaire = _questionnaireReader
        .Where(q =>
          q.CreatedById == userId &&
          q.Questions.FirstOrDefault(question => question.Options.FirstOrDefault(o => o.OptionId == optionId) != null) != null
        ).FirstOrDefault();
      Message = $"User[{userId}] does not own Questionnaire";
      return questionnaire != null;
    }
    public abstract int GetUserId(T model);
    public abstract int GetOptionId(T model);
  }

  public class CreateOptionOnOwnQuestionnaireRule : OptionOnOwnQuestionnaireRule<CreateOption> {
    public CreateOptionOnOwnQuestionnaireRule(IRead<Questionnaire> questionnaireReader) : base(questionnaireReader) {
    }

    public override int GetOptionId(CreateOption model) {
      return model.OptionId;
    }

    public override int GetUserId(CreateOption model) {
      return model.SavedById;
    }
  }

  public class UpdateOptionOnOwnQuestionnaireRule : OptionOnOwnQuestionnaireRule<UpdateOption> {
    public UpdateOptionOnOwnQuestionnaireRule(IRead<Questionnaire> questionnaireReader) : base(questionnaireReader) {
    }

    public override int GetOptionId(UpdateOption model) {
      return model.OptionId;
    }

    public override int GetUserId(UpdateOption model) {
      return model.SavedById;
    }
  }
}
