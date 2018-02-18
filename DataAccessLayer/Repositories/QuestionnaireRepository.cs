using DataAccessLayer.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework.Repositories
{
  public class QuestionnaireRepository : Repository<Questionnaire> {

    public QuestionnaireRepository(DbContext context) : base(context) {
    }

    public override void Save(Questionnaire entity) {
      if (entity.QuestionnaireId == default(int)) {
        EntityDbSet.Add(entity);
      } else {
        EntityDbSet.Update(entity);
      }
    }
  }
}
