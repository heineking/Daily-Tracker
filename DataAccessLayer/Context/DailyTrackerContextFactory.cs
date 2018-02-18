using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer.EntityFramework.Context {
  public class DailyTrackerContextFactory : IDesignTimeDbContextFactory<DailyTrackerContext> {

    public DailyTrackerContext Create() {
      var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

      var builder = new DbContextOptionsBuilder<DailyTrackerContext>();

      var connectionString = configuration.GetConnectionString("dailytracker");
      builder.UseSqlite(connectionString);
      return new DailyTrackerContext(builder.Options);
    }

    public DailyTrackerContext CreateDbContext(string[] args) {
      return Create();
    }
  }
}
