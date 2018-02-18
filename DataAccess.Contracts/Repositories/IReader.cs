using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Contracts.Repositories {
  public interface IReader<TEntity> where TEntity : class {
    TEntity Find(object id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
  }
}
