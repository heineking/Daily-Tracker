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
      var questionnaire = _questionnaireReader
        .Where(q =>
          q.CreatedById == option.SavedById &&
          q.Questions.FirstOrDefault(question => question.Options.FirstOrDefault(o => o.OptionId == option.OptionId) != null) != null
        ).FirstOrDefault();
      Message = $"User[{option.SavedById}] does not own Questionnaire";
      return questionnaire != null;
    }
  }

  public class CreateOptionOnOwnQuestionnaireRule : OptionOnOwnQuestionnaireRule<CreateOption> {
    public CreateOptionOnOwnQuestionnaireRule(IRead<Questionnaire> questionnaireReader) : base(questionnaireReader) {
    }
  }

  public class UpdateOptionOnOwnQuestionnaireRule : OptionOnOwnQuestionnaireRule<UpdateOption> {
    public UpdateOptionOnOwnQuestionnaireRule(IRead<Questionnaire> questionnaireReader) : base(questionnaireReader) {
    }
  }
}
