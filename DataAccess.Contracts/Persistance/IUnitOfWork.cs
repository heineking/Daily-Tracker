using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Contracts.Persistance {
  public interface IUnitOfWork {
    IDelete<Questionnaire> QuestionnaireDeleter { get; }
    ISave<Questionnaire> QuestionnaireSaver { get; }

    void Commit();
  }
}
