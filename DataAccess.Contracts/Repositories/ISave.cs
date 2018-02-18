using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Contracts.Repositories {
  public interface ISave<TEntity> where TEntity : class {
    void Save(TEntity entity);
  }
}
