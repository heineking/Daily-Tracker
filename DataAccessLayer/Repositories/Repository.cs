using DataAccess.Contracts.Repositories;
using DataAccess.Contracts.Strategies;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.EntityFramework.Context;

namespace DataAccessLayer.EntityFramework.Repositories {

  public abstract class Repository<TEntity> : IRead<TEntity>, ISave<TEntity>, IDelete<TEntity> where TEntity : class {

    private readonly DbContext Context;
    private readonly IEntityPredicate _entityPredicate;

    protected readonly DbSet<TEntity> EntityDbSet;

    public Repository(DbContext context, IEntityPredicate entityPredicate) {
      Context = context;
      EntityDbSet = Context.Set<TEntity>();
      _entityPredicate = entityPredicate;
    }

    public void Delete(TEntity entity) {
      EntityDbSet.Remove(entity);
    }

    public void DeleteWhere(Expression<Func<TEntity, bool>> predicate) {
      var entities = EntityDbSet.Where(predicate);
      EntityDbSet.RemoveRange(entities);
    }

    public IEnumerable<TEntity> GetAll() {
      return EntityDbSet.ToList();
    }

    public TEntity GetById(object id) {
      return EntityDbSet.Find(id);
    }

    public void Save(TEntity entity) {
      if (_entityPredicate.IsNewEntity(entity)) {
        EntityDbSet.Add(entity);
      } else {
        EntityDbSet.Update(entity);
      }
    }

    public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) {
      return EntityDbSet.Where(predicate);
    }
  }
}
