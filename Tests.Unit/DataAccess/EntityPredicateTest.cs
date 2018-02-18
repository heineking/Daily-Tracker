using DataAccess.Contracts.Strategies;
using DataAccessLayer.Contracts.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unit.DataAccess {
  [TestClass]
  public class EntityPredicateTest {

    EntityPredicate _entityPredicate;

    public EntityPredicateTest() {
      _entityPredicate = new EntityPredicate();
    }

    [TestMethod]
    public void GetPrimaryKey_Should_Return_The_Property_That_Begins_With_EntityName_And_Ends_With_Id() {
      // arrange
      var questionnaire = new Questionnaire { QuestionnaireId = 1 };

      // act
      var val = _entityPredicate.GetPrimaryKey(questionnaire);

      // assert
      Assert.AreEqual(val, questionnaire.QuestionnaireId);
    }

    [TestMethod]
    public void IsEntityNew_Should_Return_True_If_Entity_Id_Is_Unset() {
      // arrange
      var questionnaire = new Questionnaire();

      // act
      var shouldBeTrue = _entityPredicate.IsNewEntity(questionnaire);

      // assert
      Assert.IsTrue(shouldBeTrue);
    }

    [TestMethod]
    public void IsEntityNew_Should_Return_False_If_Entity_Id_Is_Set() {
      // arrange
      var questionnaire = new Questionnaire { QuestionnaireId = 1 };

      // act
      var shouldBeFalse = _entityPredicate.IsNewEntity(questionnaire);

      // assert
      Assert.IsFalse(shouldBeFalse);
    }
  }
}
