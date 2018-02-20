using DataAccess.Contracts.Persistance;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contracts.Repositories;
using DataAccessLayer.Contracts.Entities;

namespace DataAccessLayer.EntityFramework.Persistence {
  public class UnitOfWork : IUnitOfWork {
    private readonly DbContext _context;

    public IDelete<Questionnaire> QuestionnaireDeleter { get; }

    public ISave<Questionnaire> QuestionnaireSaver { get; }

    public UnitOfWork(DbContext context, IDelete<Questionnaire> questionnaireDeleter, ISave<Questionnaire> questionnaireSaver) {
      _context = context;
      QuestionnaireDeleter = questionnaireDeleter;
      QuestionnaireSaver = questionnaireSaver;
    }

    public void Commit() {
      _context.SaveChanges();
    }
  }
}
