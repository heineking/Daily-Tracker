using DataAccess.Contracts.Strategies;
using DataAccessLayer.Contracts.Entities;
using DataAccessLayer.EntityFramework.Context;
using DataAccessLayer.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Integrations {
  [TestClass]
  public class QuestionnaireRepositoryTests : IDisposable {
    private readonly DailyTrackerContext _context;
    private readonly QuestionnaireRepository _repo;

    // Add some questionnaires
    Func<int, Questionnaire> questionnaire = (id) => new Questionnaire {
      QuestionnaireId = id,
      CreatedDate = DateTime.Parse("2018-02-17"),
      Description = $"Test Questionnaire {id}",
      Name = $"Test {id}",
      Questions = new List<Question> {
        new Question {
          QuestionText = $"Test Question {id}",
          Options = new List<Option> {
            new Option {
              OptionValue = 1,
              OptionText = $"Option {id}"
            }
          }
        }
      }
    };

    public QuestionnaireRepositoryTests() {
      var builder = new DbContextOptionsBuilder<DailyTrackerContext>()
        .UseSqlite("Data Source=test.db");

      _context = new DailyTrackerContext(builder.Options);
      _context.Database.Migrate();
      _repo = new QuestionnaireRepository(_context, new EntityPredicate());
    }

    [TestMethod]
    public void Should_Be_Able_To_Update_Untracked_Entities() {
      var builder = new DbContextOptionsBuilder<DailyTrackerContext>()
        .UseSqlite("Data Source=test.db");

      // arrange
      var q = questionnaire(default(int));
      var context = new DailyTrackerContext(builder.Options);
      var repo = new QuestionnaireRepository(_context, new EntityPredicate());
      repo.Save(q);
      context.SaveChanges();

      // act
      var questionnaireId = q.QuestionnaireId;

      var nextContext = new DailyTrackerContext(builder.Options);
      var nextRepo = new QuestionnaireRepository(nextContext, new EntityPredicate());

      var updatedQ = new Questionnaire { Name = "Updated", QuestionnaireId = questionnaireId };
      nextRepo.Save(updatedQ);

      // assert
      var updatedFromDb = nextRepo.GetById(questionnaireId);

      Assert.AreEqual(updatedFromDb.Name, "Updated");
    }


    [TestMethod]
    public void Questionnaire_Repo_Integration_Tests() {
      // arrange
      var q1 = questionnaire(default(int));
      var q2 = questionnaire(default(int));

      // act :: insert
      _repo.Save(q1);
      _repo.Save(q2);
      _context.SaveChanges();

      // assert :: insert
      Assert.AreNotEqual(q1.QuestionnaireId, default(int));

      // act :: select_by_id
      var fromDb = _repo.GetById(q1.QuestionnaireId);

      Assert.AreEqual(fromDb.QuestionnaireId, q1.QuestionnaireId);
      Assert.AreEqual(fromDb.Name, q1.Name);
      Assert.AreEqual(fromDb.Description, q1.Description);
      Assert.AreEqual(fromDb.CreatedDate, q1.CreatedDate);

      // act :: update
      fromDb.Name = "foo bar";
      fromDb.CreatedDate = DateTime.Parse("2018-02-11");
      fromDb.Description = "foo bar";
      var question = fromDb.Questions.First();
      fromDb.Questions.Remove(question);
      fromDb.Questions.Add(new Question {
        QuestionText = "New Question"
      });

      _repo.Save(fromDb);
      _context.SaveChanges();

      // assert :: update
      var updatedFromDb = _repo.GetById(fromDb.QuestionnaireId);

      Assert.AreEqual(updatedFromDb.QuestionnaireId, q1.QuestionnaireId);
      Assert.AreEqual(updatedFromDb.Name, "foo bar");
      Assert.AreEqual(updatedFromDb.Description, "foo bar");
      Assert.AreEqual(updatedFromDb.CreatedDate, DateTime.Parse("2018-02-11"));
      Assert.AreEqual(updatedFromDb.Questions.ElementAtOrDefault(0).QuestionText, "New Question");

      // arrange
      var entities = _repo
        .Where(entity => entity.QuestionnaireId == q1.QuestionnaireId)
        .ToList();

      Assert.IsNotNull(entities);
      Assert.AreEqual(entities.Count, 1);

      // act :: deleteWhere
      _repo.DeleteWhere(entity => entity.QuestionnaireId == q1.QuestionnaireId);
      _context.SaveChanges();

      // assert :: deleteWhere
      var deletedEntity = _repo.GetById(q1.QuestionnaireId);
      Assert.IsNull(deletedEntity);

      // assert :: no exception for deleteWhere (where predicate returns false)
      _repo.DeleteWhere(entity => false);

      // arrange :: delete
      var toBeDeletedEntity = _repo.GetAll().ElementAtOrDefault(0);

      // act
      Assert.IsNotNull(toBeDeletedEntity);
      _repo.Delete(toBeDeletedEntity);
      _context.SaveChanges();

      // assert
      deletedEntity = _repo.GetById(toBeDeletedEntity.QuestionnaireId);
      Assert.IsNull(deletedEntity);

    }

    public void Dispose() {
      _context.Database.EnsureDeleted();
    }
  }
}
