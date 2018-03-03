using Commands.Events;
using Commands.ValidationHandlers;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.Validators {
  public class OnlySaveOwnQuestionnaireRule : IRule<UpdateQuestionnaire> {
    private readonly IRead<Questionnaire> _questionnaireReader;
    public OnlySaveOwnQuestionnaireRule(IRead<Questionnaire> questionnaireReader) {
      _questionnaireReader = questionnaireReader;
    }

    public string Message { get; set; }
    public Func<UpdateQuestionnaire, bool> IsValid { get { return UserOwnsQuestionnaire; } set { } }

    public bool UserOwnsQuestionnaire(UpdateQuestionnaire updateQuestionnaire) {
      var questionnaire = _questionnaireReader.Where(q => q.QuestionnaireId == updateQuestionnaire.QuestionnaireId).Single();
      Message = $"Cannot save questionnaire that does not belong to user";
      return questionnaire.CreatedById == updateQuestionnaire.SavedById;
    }
  }
}
