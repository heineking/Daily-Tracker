using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Contracts.Repositories {
  public interface IRead<TEntity> where TEntity : class {
    TEntity GetById(object id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
  }
}
