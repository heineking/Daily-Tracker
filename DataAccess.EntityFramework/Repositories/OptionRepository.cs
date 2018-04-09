using DataAccessLayer.Contracts.Entities;
using DataAccessLayer.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Contracts.Strategies;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories {
  public class OptionRepository : Repository<Option> {
    public OptionRepository(DbContext context, IEntityPredicate entityPredicate) : base(context, entityPredicate) {
    }
  }
}
