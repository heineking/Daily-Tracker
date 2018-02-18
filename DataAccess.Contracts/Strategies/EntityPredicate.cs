using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Contracts.Strategies {
  public class EntityPredicate : IEntityPredicate {

    public bool IsNewEntity<TEntity>(TEntity entity) {
      var pk = GetPrimaryKey(entity);
      var defaultValue = pk.GetType().IsValueType ? Activator.CreateInstance(pk.GetType()) : null;

      return pk.Equals(defaultValue);
    }

    public object GetPrimaryKey<TEntity>(TEntity entity) {
      var entityType = typeof(TEntity);
      return entityType
        .GetProperties()
        .Single(prop => string.Equals(prop.Name, $"{entityType.Name}Id", StringComparison.OrdinalIgnoreCase))
        .GetValue(entity, null);
    }

  }
}
