using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Contracts.Persistance {
  public interface IQuestionnaireRepository {
    IDelete<Questionnaire> Deleter { get; }
    ISave<Questionnaire> Saver { get; }
    IRead<Questionnaire> Reader { get; }
  }
}
