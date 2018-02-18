using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Contracts.Repositories {
  public interface IDelete<TEntity> where TEntity : class {
    void Delete(TEntity entity);
    void DeleteWhere(Expression<Func<TEntity, bool>> predicate);
  }
}
