using DataAccess.Contracts.Repositories;
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
    protected readonly DbSet<TEntity> EntityDbSet;

    public Repository(DbContext context) {
      Context = context;
      EntityDbSet = Context.Set<TEntity>();
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

    public abstract void Save(TEntity entity);

    public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) {
      return EntityDbSet.Where(predicate);
    }
  }
}
