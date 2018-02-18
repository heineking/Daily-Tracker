using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Contracts.Strategies {
  public interface IEntityPredicate {
    bool IsNewEntity<TEntity>(TEntity entity);
  }
}
