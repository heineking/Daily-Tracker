using DataAccessLayer.Contracts.Entities;
using DataAccessLayer.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Contracts.Strategies;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories {
  public class UserDirectoryRepository : Repository<UserDirectory> {
    public UserDirectoryRepository(DbContext context, IEntityPredicate entityPredicate) : base(context, entityPredicate) {
    }
  }
}
