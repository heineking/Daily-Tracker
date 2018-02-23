using DataAccessLayer.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EntityFramework.Context {
  public class DailyTrackerContext : DbContext {

    public DailyTrackerContext(DbContextOptions options) : base(options) {
      
    }
    
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDirectory> UserDirectory { get; set; }
  }
}
