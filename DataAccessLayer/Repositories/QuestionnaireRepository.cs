using DataAccess.Contracts.Strategies;
using DataAccessLayer.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework.Repositories
{
  public class QuestionnaireRepository : Repository<Questionnaire> {

    public QuestionnaireRepository(DbContext context, IEntityPredicate entityPredicate) : base(context, entityPredicate) {
    }
  }
}
